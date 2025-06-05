﻿using Newtonsoft.Json;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using PokeViewer.NET.Properties;
using SysBot.Base;
using System.Text;
using static PokeViewer.NET.RoutineExecutor;
using static SysBot.Base.SwitchButton;
using static SysBot.Base.SwitchStick;

namespace PokeViewer.NET.SubForms
{
    public partial class Egg_Viewer : Form
    {
        private readonly ViewerState Executor;
        public int GameType;
        protected ViewerOffsets Offsets { get; } = new();
        public Egg_Viewer(int gametype, ViewerState executor, (Color, Color) color)
        {
            InitializeComponent();
            GameType = gametype;
            Executor = executor;
            SetColors(color);
        }

        private (Color, Color) SetColor;
        private int eggcount = 0;
        private int sandwichcount = 0;
        private int starcount = 0;
        private int squarecount = 0;
        private ulong OverworldOffset;
        private string SpriteUrl = string.Empty;
        private PK9 prevPK = new();
        private static readonly PK9 Blank = new();
        private readonly byte[] BlankVal = { 0x01 };
        private List<Species> SpeciesResults = new();
        private List<Image> SpriteResults = new();
        private ulong BoxStartOffset;
        private DateTime StartTime;

        private void SetColors((Color, Color) color)
        {
            BackColor = color.Item1;
            ForeColor = color.Item2;
            FetchButton.BackColor = color.Item1;
            FetchButton.ForeColor = color.Item2;
            HardStopButton.BackColor = color.Item1;
            HardStopButton.ForeColor = color.Item2;
            StopConditionsButton.BackColor = color.Item1;
            StopConditionsButton.ForeColor = color.Item2;
            ScreenshotButton.BackColor = color.Item1;
            ScreenshotButton.ForeColor = color.Item2;
            PokeStats.BackColor = color.Item1;
            PokeStats.ForeColor = color.Item2;
            EatOnStart.BackColor = color.Item1;
            EatOnStart.ForeColor = color.Item2;
            EatAgain.BackColor = color.Item1;
            EatAgain.ForeColor = color.Item2;
            HidePIDEC.BackColor = color.Item1;
            HidePIDEC.ForeColor = color.Item2;
            HoldTimeToFillings.BackColor = color.Item1;
            HoldTimeToFillings.ForeColor = color.Item2;
            DisplayPartyCheck.BackColor = color.Item1;
            DisplayPartyCheck.ForeColor = color.Item2;
            FetchButton.BackColor = color.Item1;
            FetchButton.ForeColor = color.Item2;
            DUPItem1Check.BackColor = color.Item1;
            DUPItem1Check.ForeColor = color.Item2;
            DUPItem2Check.BackColor = color.Item1;
            DUPItem2Check.ForeColor = color.Item2;
            DUPItem3Check.BackColor = color.Item1;
            DUPItem3Check.ForeColor = color.Item2;
            CollectEggsCheck.BackColor = color.Item1;
            CollectEggsCheck.ForeColor = color.Item2;
            SetColor = color;
        }

        private void SanityCheck(PKM pk, int count)
        {
            PictureBox[] boxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };
            bool isValid = PersonalTable.SV.IsPresentInGame(pk.Species, pk.Form);
            string? sprite;
            if (!isValid || pk.Species <= 0 || pk.Species > (int)Species.MAX_COUNT)
            {
                sprite = "https://raw.githubusercontent.com/kwsch/PKHeX/master/PKHeX.Drawing.PokeSprite/Resources/img/Pokemon%20Sprite%20Overlays/starter.png";
                boxes[count].Load(sprite);
                boxes[count].SizeMode = PictureBoxSizeMode.CenterImage;
                return;
            }
            try
            {
                sprite = PokeImg(pk, false);
            }
            catch
            {
                sprite = "https://raw.githubusercontent.com/kwsch/PKHeX/master/PKHeX.Drawing.PokeSprite/Resources/img/Pokemon%20Sprite%20Overlays/starter.png";
            }
            boxes[count].Load(sprite);
            boxes[count].SizeMode |= PictureBoxSizeMode.Zoom;
            return;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            UptimeOnLoad(sender, e);
            await EggStart(token).ConfigureAwait(false);
        }

        public async Task EggStart(CancellationToken token)
        {
            if (DisplayPartyCheck.Checked)
                await GatherPokeParty(token).ConfigureAwait(false);
            StartTime = DateTime.Now;
            await PerformEggRoutine(token).ConfigureAwait(false);
        }

        private void UptimeOnLoad(object sender, EventArgs e)
        {
            var timer = new System.Timers.Timer { Interval = 1000 };
            timer.Elapsed += (o, args) =>
            {
                UptimeLabel.Text = $"Uptime: {StartTime - DateTime.Now:d\\.hh\\:mm\\:ss}";
            };
            timer.Start();
        }

        private async Task SetupBoxState(CancellationToken token)
        {
            var existing = await ReadBoxPokemonSV(BoxStartOffset, 0x158, token).ConfigureAwait(false);
            if (existing.Species != 0 && existing.ChecksumValid)
                ViewerUtil.DumpPokemon(AppDomain.CurrentDomain.BaseDirectory, "saved", existing);

            await SetBoxPokemonEgg(Blank, BoxStartOffset, token).ConfigureAwait(false);
        }

        private async Task SetCurrentBox(byte box, CancellationToken token)
        {
            await Executor.SwitchConnection.PointerPoke(new[] { box }, Offsets.CurrentBoxSV, token).ConfigureAwait(false);
        }

        public async Task SetBoxPokemonEgg(PK9 pkm, ulong ofs, CancellationToken token)
        {
            pkm.ResetPartyStats();
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(pkm.EncryptedPartyData, ofs, token).ConfigureAwait(false);
        }

        private async Task InitializeSessionOffsets(CancellationToken token)
        {
            BoxStartOffset = await Executor.SwitchConnection.PointerAll(Offsets.BoxStartSV, token).ConfigureAwait(false);
        }

        private async Task PerformEggRoutine(CancellationToken token)
        {
            if (FetchButton.Enabled)
                DisableOptions();

            eggcount = 0;
            await Executor.SwitchConnection.WriteBytesMainAsync(BlankVal, Offsets.PicnicMenu, token).ConfigureAwait(false);
            prevPK = await ReadPokemonSV(Offsets.EggData, 344, token).ConfigureAwait(false);
            if (CollectEggsCheck.Checked)
            {
                await InitializeSessionOffsets(token).ConfigureAwait(false);
                await SetupBoxState(token).ConfigureAwait(false);
                await SetCurrentBox(0, token).ConfigureAwait(false);
            }
            try
            {
                if (EatOnStart.Checked)
                {
                    await MakeSandwich(token).ConfigureAwait(false);
                    if (CollectEggsCheck.Checked)
                        await WaitAndCollectEggs(token).ConfigureAwait(false);
                    else
                        await WaitForEggs(token).ConfigureAwait(false);
                }
                else
                {
                    if (CollectEggsCheck.Checked)
                        await WaitAndCollectEggs(token).ConfigureAwait(false);
                    else
                        await WaitForEggs(token).ConfigureAwait(false);
                }

                return;
            }
            catch (Exception ex)
            {
                EnableOptions();
                MessageBox.Show($"{ex}");
            }
            return;
        }

        private async Task WaitAndCollectEggs(CancellationToken token)
        {
            PK9 pkprev = new();
            SpriteUrl = string.Empty;
            while (!token.IsCancellationRequested)
            {
                var wait = TimeSpan.FromMinutes(31);
                var endTime = DateTime.Now + wait;
                var waiting = 0;
                NextSanwichLabel.Text = $"Next Sandwich: {endTime:hh\\:mm\\:ss}";
                while (DateTime.Now < endTime)
                {
                    BasketCount.Text = "Status: Waiting..";
                    var pk = await ReadPokemonSV(Offsets.EggData, 344, token).ConfigureAwait(false);
                    while (pk == prevPK || pk == null || pkprev!.EncryptionConstant == pk.EncryptionConstant || (Species)pk.Species == Species.None)
                    {
                        if (DateTime.Now >= endTime)
                            break;

                        await Task.Delay(1_500, token).ConfigureAwait(false);
                        pk = await ReadPokemonSV(Offsets.EggData, 344, token).ConfigureAwait(false);
                        waiting++;

                        if (waiting == 120)
                        {
                            await Click(A, 1_500, token).ConfigureAwait(false);
                            waiting = 0;
                        }
                    }
                    BasketCount.Text = "Status: Checking..";
                    var (match, enc) = await RetrieveEgg(token).ConfigureAwait(false);

                    if (enc.Species != (ushort)Species.None)
                    {
                        PokeStats.Text = GetPrint(enc);

                        if (SpriteResults.Count is not 0)
                        {
                            for (int k = 0; k < SpriteResults.Count; k++)
                            {
                                if (SpeciesResults[k] == (Species)enc.Species)
                                {
                                    if (!pk!.IsShiny)
                                        PokeSpriteBox.Image = SpriteResults[k];
                                    else
                                    {
                                        SpriteUrl = PokeImg(pk, false);
                                        PokeSpriteBox.Load(SpriteUrl);
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                SpriteUrl = PokeImg(enc, false);
                            }
                            catch
                            {
                                SpriteUrl = "https://raw.githubusercontent.com/kwsch/PKHeX/master/PKHeX.Drawing.PokeSprite/Resources/img/Pokemon%20Sprite%20Overlays/starter.png";
                            }
                            Image image = null!;
                            using (var httpClient = new HttpClient())
                            {
                                var imageContent = await httpClient.GetByteArrayAsync(SpriteUrl, token);
                                using var imageBuffer = new MemoryStream(imageContent);
                                image = Image.FromStream(imageBuffer);
                            }

                            SpriteResults.Add(image);
                            SpeciesResults.Add((Species)enc.Species);
                            PokeSpriteBox.Image = image;
                        }
                        var ballsprite = SpriteUtil.GetBallSprite(enc.Ball);
                        BallBox.Image = ballsprite;

                        if (!match)
                        {
                            if (enc.IsShiny)
                            {
                                var shinyurl = PokeImg(enc, false);
                                PokeSpriteBox.Load(shinyurl);
                                SendNotifications(PokeStats.Text, shinyurl, match);
                            }
                        }
                        if (match)
                        {
                            prevPK = enc;
                            await Click(HOME, 0_500, token).ConfigureAwait(false);
                            if (enc.IsShiny)
                            {
                                var shinyurl = PokeImg(enc, false);
                                PokeSpriteBox.Load(shinyurl);
                                SendNotifications(PokeStats.Text, shinyurl, match);
                            }
                            else
                                SendNotifications(PokeStats.Text, SpriteUrl, match);
                            EnableOptions();
                            Activate();
                            MessageBox.Show("Match found! Egg should be first one in your boxes! Double check with BoxView! " +
                                "Make sure to move your match to a different spot from Box 1 Slot 1 or it will be deleted on the next bot start.");
                            return;
                        }
                        if (!match)
                            await SetBoxPokemonEgg(Blank, BoxStartOffset, token).ConfigureAwait(false);

                        pkprev = pk!;
                        waiting = 0;
                    }
                }
                if (EatAgain.Checked)
                {
                    BasketCount.Text = "Status: Time to eat..";
                    await MakeSandwich(token).ConfigureAwait(false);
                }
                else
                    BasketCount.Text = "Status: Done.";
            }
        }

        private async Task WaitForEggs(CancellationToken token)
        {
            PK9 pkprev = new();
            SpriteUrl = string.Empty;
            while (!token.IsCancellationRequested)
            {
                var wait = TimeSpan.FromMinutes(30);
                var endTime = DateTime.Now + wait;
                var waiting = 0;
                var ctr = 0;
                NextSanwichLabel.Text = $"Next Sandwich: {endTime:hh\\:mm\\:ss}";
                while (DateTime.Now < endTime)
                {
                    BasketCount.Text = $"Basket Count: {ctr}";
                    var pk = await ReadPokemonSV(Offsets.EggData, 344, token).ConfigureAwait(false);
                    while (pk == prevPK || pk == null || pkprev.EncryptionConstant == pk.EncryptionConstant || (Species)pk.Species == Species.None)
                    {
                        waiting++;
                        await Task.Delay(1_500, token).ConfigureAwait(false);
                        pk = await ReadPokemonSV(Offsets.EggData, 344, token).ConfigureAwait(false);
                        waiting++;

                        if (waiting == 200)
                        {
                            await ReopenPicnic(token).ConfigureAwait(false);
                            await MakeSandwich(token).ConfigureAwait(false);
                            await ReopenPicnic(token).ConfigureAwait(false);
                            wait = TimeSpan.FromMinutes(30);
                            endTime = DateTime.Now + wait;
                            NextSanwichLabel.Text = $"Next Sandwich: {endTime:hh\\:mm\\:ss}";
                            waiting = 0;
                            ctr = 0;
                        }

                    }

                    pk = await ReadPokemonSV(Offsets.EggData, 344, token).ConfigureAwait(false);
                    while (pk != null && (Species)pk.Species != Species.None && pkprev.EncryptionConstant != pk.EncryptionConstant)
                    {
                        waiting = 0;
                        ctr++;
                        eggcount++;
                        PokeStats.Text = GetPrint(pk);

                        if (SpriteResults.Count is not 0)
                        {
                            for (int k = 0; k < SpriteResults.Count; k++)
                            {
                                if (SpeciesResults[k] == (Species)pk.Species)
                                {
                                    if (!pk!.IsShiny)
                                        PokeSpriteBox.Image = SpriteResults[k];
                                    else
                                    {
                                        SpriteUrl = PokeImg(pk, false);
                                        PokeSpriteBox.Load(SpriteUrl);
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                SpriteUrl = PokeImg(pk, false);
                            }
                            catch
                            {
                                SpriteUrl = "https://raw.githubusercontent.com/kwsch/PKHeX/master/PKHeX.Drawing.PokeSprite/Resources/img/Pokemon%20Sprite%20Overlays/starter.png";
                            }
                            Image image = null!;
                            using (var httpClient = new HttpClient())
                            {
                                var imageContent = await httpClient.GetByteArrayAsync(SpriteUrl, token);
                                using var imageBuffer = new MemoryStream(imageContent);
                                image = Image.FromStream(imageBuffer);
                            }

                            SpriteResults.Add(image);
                            SpeciesResults.Add((Species)pk.Species);
                            PokeSpriteBox.Image = image;
                        }

                        var ballsprite = SpriteUtil.GetBallSprite(pk.Ball);
                        BallBox.Image = ballsprite;
                        bool match = ValidateEncounter(pk);
                        if (!match)
                        {
                            if (pk.IsShiny)
                            {
                                var shinyurl = PokeImg(pk, false);
                                PokeSpriteBox.Load(shinyurl);
                                SendNotifications(PokeStats.Text, shinyurl, match);
                            }
                        }
                        if (match)
                        {
                            prevPK = pk;
                            if (pk.IsShiny)
                            {
                                var shinyurl = PokeImg(pk, false);
                                PokeSpriteBox.Load(shinyurl);
                                SendNotifications(PokeStats.Text, shinyurl, match);
                            }
                            else
                                SendNotifications(PokeStats.Text, SpriteUrl, match);
                            EnableOptions();
                            Activate();
                            MessageBox.Show("Match found! Make sure to move your match to a different spot from Box 1 Slot 1 or it will be deleted on the next bot start.");
                            return;
                        }
                        pkprev = pk;
                    }
                    if (ctr == 10)
                    {
                        BasketCount.Text = $"Resetting..";
                        await ReopenPicnic(token).ConfigureAwait(false);
                        ctr = 0;
                        waiting = 0;
                        BasketCount.Text = $"Basket Count: {ctr}";
                    }
                }
                await MakeSandwich(token).ConfigureAwait(false);
            }
        }

        private string GetPrint(PK9 pk)
        {
            string pid = $"{Environment.NewLine}PID: {pk.PID:X8}";
            string ec = $"{Environment.NewLine}EC: {pk.EncryptionConstant:X8}";
            var form = FormOutput(pk.Species, pk.Form, out _);
            string gender = string.Empty;
            switch (pk.Gender)
            {
                case 0: gender = " (M)"; break;
                case 1: gender = " (F)"; break;
                case 2: break;
            }
            string sensitiveinfo = HidePIDEC.Checked ? "" : $"{pid}{ec}";
            return $"{$"Egg #{eggcount}"}{Environment.NewLine}{(pk.ShinyXor == 0 ? "■ - " : pk.ShinyXor <= 16 ? "★ - " : "")}{(Species)pk.Species}{form}{gender}{sensitiveinfo}{Environment.NewLine}Nature: {(Nature)pk.Nature}{Environment.NewLine}Ability: {GameInfo.GetStrings(1).Ability[pk.Ability]}{Environment.NewLine}IVs: {pk.IV_HP}/{pk.IV_ATK}/{pk.IV_DEF}/{pk.IV_SPA}/{pk.IV_SPD}/{pk.IV_SPE}{Environment.NewLine}Scale: {PokeSizeDetailedUtil.GetSizeRating(pk.Scale)} ({pk.Scale})";

        }

        private async Task<(bool, PK9)> RetrieveEgg(CancellationToken token)
        {
            bool match = false;
            for (int a = 0; a < 4; a++)
                await Click(A, 1_500, token).ConfigureAwait(false);

            await Task.Delay(0_500, token).ConfigureAwait(false);
            PK9 pk = await ReadBoxPokemonSV(BoxStartOffset, 344, token).ConfigureAwait(false);
            if ((Species)pk.Species != Species.None)
            {
                BasketCount.Text = "Status: Egg found!";
                eggcount++;
                match = ValidateEncounter(pk);
                if (ForceDumpCheck.Checked)
                    ViewerUtil.DumpPokemon(AppDomain.CurrentDomain.BaseDirectory, "eggs", pk);
                await Click(MINUS, 1_500, token).ConfigureAwait(false);
                for (int a = 0; a < 2; a++)
                    await Click(A, 1_500, token).ConfigureAwait(false);
                return (match, pk);
            }

            else
            {
                BasketCount.Text = "Status: No egg :(";
                for (int a = 0; a < 2; a++)
                    await Click(A, 1_500, token).ConfigureAwait(false);
            }

            return (match, pk);
        }

        private bool ValidateEncounter(PK9 pk)
        {
            bool IVmatches = false;
            if (pk.IsShiny)
            {
                if (pk.ShinyXor == 0)
                    squarecount++;
                else
                    starcount++;
                ShinyFoundLabel.Text = $"Shinies Found: {squarecount + starcount}";
                SquareStarCount.Text = $"■ - {squarecount} | ★ - {starcount}";
            }

            if (Settings.Default.ApplyIVFilter)
            {
                if (Settings.Default.MatchShinyIV is false && pk.IsShiny && Settings.Default.ShinyFilter != 0 && Settings.Default.ShinyFilter != 1 && IVmatches == false)
                    return true;

                if (Settings.Default.HpCon is 0 && pk.IV_HP != Settings.Default.HPFilter || Settings.Default.AtkCon is 0 && pk.IV_ATK != Settings.Default.AtkFilter || Settings.Default.DefCon is 0 && pk.IV_DEF != Settings.Default.DefFilter ||
                    Settings.Default.SpaCon is 0 && pk.IV_SPA != Settings.Default.SpaFilter || Settings.Default.SpdCon is 0 && pk.IV_SPD != Settings.Default.SpdFilter || Settings.Default.SpeCon is 0 && pk.IV_SPE != Settings.Default.SpeFilter)
                    return false;

                if (Settings.Default.HpCon is 1 && pk.IV_HP > Settings.Default.HPFilter || Settings.Default.AtkCon is 1 && pk.IV_ATK > Settings.Default.AtkFilter || Settings.Default.DefCon is 1 && pk.IV_DEF > Settings.Default.DefFilter ||
                    Settings.Default.SpaCon is 1 && pk.IV_SPA > Settings.Default.SpaFilter || Settings.Default.SpdCon is 1 && pk.IV_SPD > Settings.Default.SpdFilter || Settings.Default.SpeCon is 1 && pk.IV_SPE > Settings.Default.SpeFilter)
                    return false;

                if (Settings.Default.HpCon is 2 && pk.IV_HP < Settings.Default.HPFilter || Settings.Default.AtkCon is 2 && pk.IV_ATK < Settings.Default.AtkFilter || Settings.Default.DefCon is 2 && pk.IV_DEF < Settings.Default.DefFilter ||
                    Settings.Default.SpaCon is 2 && pk.IV_SPA < Settings.Default.SpaFilter || Settings.Default.SpdCon is 2 && pk.IV_SPD < Settings.Default.SpdFilter || Settings.Default.SpeCon is 2 && pk.IV_SPE < Settings.Default.SpeFilter)
                    return false;

                IVmatches = true;
            }

            if (pk.Gender != Settings.Default.GenderFilter && Settings.Default.GenderFilter != 3)
                return false; // gender != gender filter when gender is not Any

            if (pk.Nature != (Nature)Settings.Default.NatureFilter && Settings.Default.NatureFilter != 25)
                return false; // nature != nature filter when nature is not Random

            if (Settings.Default.MinMaxOnly && pk.Scale > 0 && pk.Scale < 255) // Mini/Jumbo Only
                return false;

            if (Settings.Default.MatchShinyIV is false && !pk.IsShiny && Settings.Default.ShinyFilter != 0 && Settings.Default.ShinyFilter != 1 && IVmatches == true)
                return true;

            if (!pk.IsShiny && Settings.Default.ShinyFilter != 0 && Settings.Default.ShinyFilter != 1)
                return false;

            if (!pk.IsShiny && Settings.Default.ShinyFilter == 0 || !pk.IsShiny && Settings.Default.ShinyFilter == 1)
                return true;

            if (pk.IsShiny && Settings.Default.ShinyFilter is not 0 or 1)
            {
                if (Settings.Default.ShinyFilter is 4 && pk.ShinyXor != 0) // SquareOnly
                    return false;

                if (Settings.Default.ShinyFilter is 3 && pk.ShinyXor > 0 && pk.ShinyXor > 16) // StarOnly
                    return false;

                if ((Species)pk.Species is Species.Dunsparce or Species.Tandemaus && pk.EncryptionConstant % 100 != 0 && Settings.Default.SegmentOrFamily)
                    return false;

                if ((Species)pk.Species is Species.Dunsparce or Species.Tandemaus && pk.EncryptionConstant % 100 == 0 && Settings.Default.SegmentOrFamily)
                    return true;
            }

            return true;
        }

        private async Task<PK9> ReadPokemonSV(uint offset, int size, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesMainAsync(offset, size, token).ConfigureAwait(false);
            var pk = new PK9(data);
            return pk;
        }

        private async Task SetStick(SwitchStick stick, short x, short y, int delay, CancellationToken token)
        {
            var cmd = SwitchCommand.SetStick(stick, x, y, true);
            await Executor.SwitchConnection.SendAsync(cmd, token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private async Task MakeSandwich(CancellationToken token)
        {
            await Click(MINUS, 0_500, token).ConfigureAwait(false);
            await SetStick(LEFT, 0, 32323, 0_700, token).ConfigureAwait(false); // Face up to table
            await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);
            await Task.Delay(1_000, token).ConfigureAwait(false);
            await Click(A, 1_500, token).ConfigureAwait(false);
            await Click(A, 1_500, token).ConfigureAwait(false); // Dummy press if we're in union circle, doesn't affect routine
            await Click(A, 10_000, token).ConfigureAwait(false);
            await Click(X, 2_500, token).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(Item1Value.Text))
            {
                // Lettuce
                var m1 = Convert.ToInt32(Item1Value.Text);

                for (int i = 0; i < m1; i++)
                {
                    if (DUPItem1Check.Checked)
                        await Click(DUP, 0_800, token).ConfigureAwait(false);
                    else
                        await Click(DDOWN, 0_800, token).ConfigureAwait(false);
                }
            }

            await Click(A, 0_800, token).ConfigureAwait(false);
            await Click(PLUS, 0_800, token).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(Item2Value.Text))
            {
                // Mystica Salt
                var m2 = Convert.ToInt32(Item2Value.Text);

                for (int i = 0; i < m2; i++)
                {
                    if (DUPItem2Check.Checked)
                        await Click(DUP, 0_800, token).ConfigureAwait(false);
                    else
                        await Click(DDOWN, 0_800, token).ConfigureAwait(false);
                }
            }

            await Click(A, 0_800, token).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(Item3Value.Text))
            {
                // Mystica Sweet
                var m3 = Convert.ToInt32(Item3Value.Text);

                for (int i = 0; i < m3; i++)
                {
                    if (DUPItem3Check.Checked)
                        await Click(DUP, 0_800, token).ConfigureAwait(false);
                    else
                        await Click(DDOWN, 0_800, token).ConfigureAwait(false);
                }
            }

            await Click(A, 0_800, token).ConfigureAwait(false);
            await Click(PLUS, 0_800, token).ConfigureAwait(false);
            // Set pick
            await Click(A, 8_000, token).ConfigureAwait(false);
            //Wait for bread

            var fillingtime = Convert.ToInt32(FillingHoldTime.Text);
            await SetStick(LEFT, 0, 30000, 0_000 + fillingtime, token).ConfigureAwait(false); // Navigate to ingredients
            await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);
            await Task.Delay(0_500, token).ConfigureAwait(false);

            if (HoldIngredients.Checked)
            {
                for (int i = 0; i < NumberOfFillings.Value; i++) // Amount of ingredients to drop
                {
                    await Hold(A, 0_800, token).ConfigureAwait(false);

                    await SetStick(LEFT, 0, -30000, 0_000 + fillingtime, token).ConfigureAwait(false); // Navigate to ingredients
                    await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);
                    await Task.Delay(0_500, token).ConfigureAwait(false);
                    await Release(A, 0_800, token).ConfigureAwait(false);

                    await SetStick(LEFT, 0, 30000, 0_000 + fillingtime, token).ConfigureAwait(false); // Navigate to ingredients
                    await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);
                    await Task.Delay(0_500, token).ConfigureAwait(false);
                }
            }

            for (int i = 0; i < 12; i++) // If everything is properly positioned
                await Click(A, 0_800, token).ConfigureAwait(false);

            // Sandwich failsafe
            for (int i = 0; i < 5; i++) //Attempt this several times to ensure it goes through
                await SetStick(LEFT, 0, 30000, 1_000, token).ConfigureAwait(false); // Scroll to the absolute top
            await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);

            while (await PicnicState(token).ConfigureAwait(false) == 2) // Until we start eating the sandwich
            {
                await SetStick(LEFT, 0, -5000, 0_300, token).ConfigureAwait(false); // Scroll down slightly and press A a few times; repeat until un-stuck
                await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);

                for (int i = 0; i < 6; i++)
                    await Click(A, 0_800, token).ConfigureAwait(false);
            }

            while (await PicnicState(token).ConfigureAwait(false) == 3)  // eating the sandwich
                await Task.Delay(1_000, token).ConfigureAwait(false);

            sandwichcount++;
            SandwichCount.Text = $"Sandwiches Made: {sandwichcount}";

            while (!await IsInPicnic(token).ConfigureAwait(false)) // Acknowledge the sandwich and return to the picnic            
                await Click(A, 5_000, token).ConfigureAwait(false); // Wait a long time to give the flag a chance to update and avoid sandwich re-entry    

            await Task.Delay(2_500, token).ConfigureAwait(false);
            await SetStick(LEFT, 0, -10000, 0_500, token).ConfigureAwait(false); // Face down to basket
            await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);
            await Task.Delay(1_000, token).ConfigureAwait(false);
            await SetStick(LEFT, 0, 5000, 0_200, token).ConfigureAwait(false); // Face up to basket
            await SetStick(LEFT, 0, 0, 0, token).ConfigureAwait(false);

        }

        private async Task<bool> IsOnOverworld(ulong offset, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(offset, 1, token).ConfigureAwait(false);
            return data[0] == 0x11;
        }

        private async Task ReopenPicnic(CancellationToken token)
        {
            await Task.Delay(0_500, token).ConfigureAwait(false);
            await Click(Y, 1_500, token).ConfigureAwait(false);
            var overworldWaitCycles = 0;
            var hasReset = false;
            OverworldOffset = await Executor.SwitchConnection.PointerAll(Offsets.OverworldPointer, token).ConfigureAwait(false);
            while (!await IsOnOverworld(OverworldOffset, token).ConfigureAwait(false))
            {
                await Click(A, 1_000, token).ConfigureAwait(false);
                overworldWaitCycles++;

                if (overworldWaitCycles == 10)
                {
                    for (int i = 0; i < 5; i++)
                        await Click(B, 0_500, token).ConfigureAwait(false);

                    await Click(Y, 1_500, token).ConfigureAwait(false);
                    await Click(A, 1_000, token).ConfigureAwait(false);

                    for (int i = 0; i < 4; i++)
                        await Click(B, 0_500, token).ConfigureAwait(false);
                }
            }
            for (int i = 0; i < 10; i++)
                await Click(A, 0_500, token).ConfigureAwait(false);
            await Click(X, 3_000, token).ConfigureAwait(false);
            if (hasReset)
            {
                await Click(DRIGHT, 0_500, token).ConfigureAwait(false);
                await Click(DDOWN, 0_500, token).ConfigureAwait(false);
                await Click(DDOWN, 0_500, token).ConfigureAwait(false);
            }
            await Click(A, 7_000, token).ConfigureAwait(false);
        }

        private new async Task Click(SwitchButton b, int delay, CancellationToken token)
        {
            await Executor.SwitchConnection.SendAsync(SwitchCommand.Click(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private async Task Hold(SwitchButton b, int delay, CancellationToken token)
        {
            await Executor.SwitchConnection.SendAsync(SwitchCommand.Hold(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private async Task Release(SwitchButton b, int delay, CancellationToken token)
        {
            await Executor.SwitchConnection.SendAsync(SwitchCommand.Release(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private async void HardStopButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will restart the application. Do you wish to continue?", "Hard Stop Initiated", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                await Executor.SwitchConnection.SendAsync(SwitchCommand.DetachController(true), CancellationToken.None).ConfigureAwait(false);
                Executor.Disconnect();
                Application.Restart();
            }

            else if (dialogResult == DialogResult.No)
                return;
        }

        private async Task<int> PicnicState(CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesMainAsync(Offsets.PicnicMenu, 1, token).ConfigureAwait(false);
            return data[0]; // 1 when in picnic, 2 in sandwich menu, 3 when eating, 2 when done eating
        }

        private async Task<bool> IsInPicnic(CancellationToken token)
        {
            var Data = await Executor.SwitchConnection.ReadBytesMainAsync(Offsets.PicnicMenu, 1, token).ConfigureAwait(false);
            return Data[0] == 0x01; // 1 when in picnic, 2 in sandwich menu, 3 when eating, 2 when done eating
        }

        private void DisableOptions()
        {
            FetchButton.Enabled = false;
            Item1Value.Enabled = false;
            Item2Value.Enabled = false;
            Item3Value.Enabled = false;
            DUPItem1Check.Enabled = false;
            DUPItem2Check.Enabled = false;
            DUPItem3Check.Enabled = false;
            FillingHoldTime.Enabled = false;
            NumberOfFillings.Enabled = false;
            EatOnStart.Enabled = false;
            EatAgain.Enabled = false;
            HoldIngredients.Enabled = false;
            DisplayPartyCheck.Enabled = false;
            CollectEggsCheck.Enabled = false;
        }

        private void EnableOptions()
        {
            FetchButton.Enabled = true;
            Item1Value.Enabled = true;
            Item2Value.Enabled = true;
            Item3Value.Enabled = true;
            DUPItem1Check.Enabled = true;
            DUPItem2Check.Enabled = true;
            DUPItem3Check.Enabled = true;
            FillingHoldTime.Enabled = true;
            NumberOfFillings.Enabled = true;
            EatOnStart.Enabled = true;
            EatAgain.Enabled = true;
            HoldIngredients.Enabled = true;
            DisplayPartyCheck.Enabled = true;
            CollectEggsCheck.Enabled = true;
        }

        private static HttpClient? _client;
        private static HttpClient Client
        {
            get
            {
                _client ??= new HttpClient();
                return _client;
            }
        }

        private static string[]? DiscordWebhooks;

        private async void SendNotifications(string results, string thumbnail, bool pinguser)
        {
            if (string.IsNullOrEmpty(results) || string.IsNullOrEmpty(Settings.Default.WebHook))
                return;
            DiscordWebhooks = Settings.Default.WebHook.Split(',');
            if (DiscordWebhooks == null)
                return;
            var webhook = GenerateWebhook(results, thumbnail, pinguser);
            var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
            foreach (var url in DiscordWebhooks)
                await Client.PostAsync(url, content).ConfigureAwait(false);
        }

        private static object GenerateWebhook(string results, string thumbnail, bool pinguser)
        {
            string userContent = pinguser ? $"<@{Settings.Default.UserDiscordID}>\n{Settings.Default.PingMessage}" : "";
            string title = pinguser ? $"Match Found!" : "Unwanted match..";
            var WebHook = new
            {
                username = $"PokeViewer.NET",
                content = userContent,
                embeds = new List<object>
                {
                    new
                    { title,
                        thumbnail = new
                        {
                            url = thumbnail
                        },
                        fields = new List<object>
                        {
                            new { name = "Description               ", value = results, inline = true, },
                        },
                    }
                }
            };
            return WebHook;
        }

        private void StopConditionsButton_Click(object sender, EventArgs e)
        {
            using StopConditions miniform = new(SetColor);
            miniform.ShowDialog();
        }

        private void ScreenshotButton_Click(object sender, EventArgs e)
        {
            Rectangle bounds = Bounds;
            Bitmap bmp = new(250, 260);
            DrawToBitmap(bmp, bounds);
            Bitmap CroppedImage = bmp.Clone(new(80, 30, bmp.Width - 80, bmp.Height - 30), bmp.PixelFormat);
            Clipboard.SetImage(CroppedImage);
            MessageBox.Show("Copied to clipboard!");
        }

        private async Task GatherPokeParty(CancellationToken token)
        {
            await GatherParty(token).ConfigureAwait(false);
        }

        private async Task GatherParty(CancellationToken token)
        {
            for (int i = 0; i < 6; i++)
            {
                var val = 0x30;
                switch (i)
                {
                    case 0: break;
                    case 1: val = 0x38; break;
                    case 2: val = 0x40; break;
                    case 3: val = 0x48; break;
                    case 4: val = 0x50; break;
                    case 5: val = 0x58; break;
                }
                var pointer = new long[] { 0x4763C98, 0x08, val, 0x30, 0x00 };
                var offset = await Executor.SwitchConnection.PointerAll(pointer, token).ConfigureAwait(false);
                var pk = await ReadBoxPokemonSV(offset, 0x158, token).ConfigureAwait(false);
                SanityCheck(pk, i);
            }
        }

        private async Task<PK9> ReadBoxPokemonSV(ulong offset, int size, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(offset, size, token).ConfigureAwait(false);
            var pk = new PK9(data);
            return pk;
        }

        private void ForceEgg_CheckedChanged(object sender, EventArgs e)
        {
            if (ForceDumpCheck.Checked)
                MessageBox.Show("You have enabled force dump eggs. These should not be considered legitimate and are only a backup for ghost eggs. Please do not pass these off as legitimate eggs.");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CollectEggsCheck.Checked)
                MessageBox.Show("This mode uses the legacy method of collecting the egg from the basket as soon as it spawns. If you are experiencing an unstable experience with Wi-Fi desync/lag," +
                    " please leave this unchecked.");
        }
    }
}
