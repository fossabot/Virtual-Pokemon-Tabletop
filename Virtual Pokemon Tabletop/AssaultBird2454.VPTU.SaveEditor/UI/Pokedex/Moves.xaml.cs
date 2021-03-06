﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex
{
    /// <summary>
    /// Interaction logic for Moves.xaml
    /// </summary>
    public partial class Moves : Window
    {
        public VPTU.Pokedex.Moves.MoveData MoveData;
        bool Update = false;
        private SaveManager.Data.SaveFile.PTUSaveData SaveData;

        public Moves(SaveManager.Data.SaveFile.PTUSaveData _SaveData, VPTU.Pokedex.Moves.MoveData _MoveData = null)
        {
            InitializeComponent();// Creates the Window

            SaveData = _SaveData;

            Setup();// Executes the setup script

            if (_MoveData == null)
            {
                MoveData = new VPTU.Pokedex.Moves.MoveData();// If no specified data, then create a new data class
            }
            else
            {
                Update = true;
                MoveData = _MoveData;// Save the specified data class to a variable for latter
                Load();// Load the data
            }
        }

        /// <summary>
        /// Set's up the form
        /// </summary>
        public void Setup()
        {

            #region Data Binding
            Battle_ActionType.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Action_Type));
            Battle_Class.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.MoveClass));
            Battle_Frequency.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Move_Frequency));
            Battle_Type.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Type));

            Contest_Effect.ItemsSource = Enum.GetValues(typeof(ContestManager.Data.Contest_Effects));
            Contest_Type.ItemsSource = Enum.GetValues(typeof(ContestManager.Data.Contest_Type));
            #endregion
            #region Data Defaulting
            Battle_ActionType.SelectedIndex = 0;
            Battle_Class.SelectedIndex = 0;
            Battle_Frequency.SelectedIndex = 0;
            Battle_Type.SelectedIndex = 0;

            Contest_Effect.SelectedIndex = 0;
            Contest_Type.SelectedIndex = 0;
            #endregion
        }

        /// <summary>
        /// Loads the data from the MoveData Object
        /// </summary>
        public void Load()
        {
            #region Basic Info
            Basic_Name.Text = MoveData.Name;
            Basic_Desc.Text = MoveData.Description;
            #endregion

            #region Battle Details
            Battle_ActionType.SelectedItem = MoveData.Move_ActionType;
            Battle_Class.SelectedItem = MoveData.Move_Class;
            Battle_Frequency.SelectedItem = MoveData.Move_Frequency;
            Battle_Type.SelectedItem = MoveData.Move_Type;

            Battle_AC.Value = MoveData.Move_Accuracy;
            Battle_DB.Value = (int)MoveData.Move_DamageBase;
            Battle_UseLimit.Value = MoveData.Move_Frequency_Limit;
            #endregion

            #region Contest Details
            Contest_Effect.SelectedItem = MoveData.Contest_Effect;
            Contest_Type.SelectedItem = MoveData.Contest_Type;
            #endregion

            #region Range Data
            if (MoveData.Range_Data == null) { MoveData.Range_Data = new List<VPTU.Pokedex.Moves.Move_RangeData>(); }
            #region All Adjacent Foes
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.All_Adjacent_Foes);
                AllAdj_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Any Target
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Any);
                AnyTarget_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Burst
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Burst);
                Burst_Enabled.IsChecked = Data.Enabled;
                Burst_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Cardinally Adjacent
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Cardinally_Adjacent);
                CardinallyAdj_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Close Blast
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Close_Blast);
                CloseBlast_Enabled.IsChecked = Data.Enabled;
                CloseBlast_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Cone
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Cone);
                Cone_Enabled.IsChecked = Data.Enabled;
                Cone_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Field
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Field);
                Field_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Line
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Line);
                Line_Enabled.IsChecked = Data.Enabled;
                Line_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Mele
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Melee);
                Mele_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Range
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Range);
                Range_Enabled.IsChecked = Data.Enabled;
                Range_Distance.Value = Data.Distance;
            }
            catch { }
            #endregion
            #region Ranged Blast
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Range_Blast);
                RangedBlast_Enabled.IsChecked = Data.Enabled;
                RangedBlast_Distance.Value = Data.Distance;
                RangedBlast_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Self
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Self);
                Self_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #endregion

            #region KeyWords
            try { Keyword_Aura.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Aura).Value; } catch { }
            try { Keyword_Berry.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Berry).Value; } catch { }
            try { Keyword_Blessing.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Blessing).Value; } catch { }
            try { Keyword_Coat.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Coat).Value; } catch { }
            try { Keyword_Dash.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Dash).Value; } catch { }
            try { Keyword_DubleStrike.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.DubleStrike).Value; } catch { }
            try { Keyword_Environ.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Environ).Value; } catch { }
            try { Keyword_Execute.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Execute).Value; } catch { }
            //try { Keyword_Exhaust.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == Move_KeyWords.Exhaust).Value; } catch { }
            try { Keyword_FiveStrike.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.FiveStrike).Value; } catch { }
            try { Keyword_Fling.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Fling).Value; } catch { }
            try { Keyword_Friendly.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Friendly).Value; } catch { }
            try { Keyword_GroundSource.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Groundsource).Value; } catch { }
            try { Keyword_Hail.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Hail).Value; } catch { }
            try { Keyword_Hazard.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Hazard).Value; } catch { }
            try { Keyword_Illusion.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Illusion).Value; } catch { }
            try { Keyword_Interupt.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Interupt).Value; } catch { }
            try { Keyword_Pass.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Pass).Value; } catch { }
            try { Keyword_Pledge.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Pledge).Value; } catch { }
            try { Keyword_Powder.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Powder).Value; } catch { }
            //try { Keyword_Priority_Type.SelectedItem = MoveData.KeyWords.Find(x => x.Key == Move_KeyWords.Priority).Value; } catch { }
            if (MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Priority).Value != null)
            {
                Keyword_Priority.IsChecked = true;
            }
            try { Keyword_Push.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Push).Value; } catch { }
            try { Keyword_Rainy.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Rainy).Value; } catch { }
            try { Keyword_Reaction.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Reaction).Value; } catch { }
            if (MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Recoil).Value != null)
            {
                Keyword_Recoil.IsChecked = true;
            }
            //try { Keyword_Recoil_Type.SelectedItem = MoveData.KeyWords.Find(x => x.Key == Move_KeyWords.Recoil).Value; } catch { }
            //try { Keyword_SandStorm.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == Move_KeyWords.SandStorm).Value; } catch { }
            try { Keyword_Setup.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.SetUp).Value; } catch { }
            try { Keyword_Shield.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Shield).Value; } catch { }
            try { Keyword_Smite.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Smite).Value; } catch { }
            try { Keyword_Social.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Social).Value; } catch { }
            try { Keyword_Sonic.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Sonic).Value; } catch { }
            try { Keyword_SpiritSurge.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.SpiritSurge).Value; } catch { }
            try { Keyword_Sunny.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Sunny).Value; } catch { }
            try { Keyword_Trigger.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Trigger).Value; } catch { }
            try { Keyword_Vortex.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Vortex).Value; } catch { }
            try { Keyword_Weather.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.Weather).Value; } catch { }
            try { Keyword_WeightClass.IsChecked = (bool)MoveData.KeyWords.Find(x => x.Key == VPTU.BattleManager.Data.Move_KeyWords.WeightClass).Value; } catch { }
            #endregion
        }

        /// <summary>
        /// Saves the data to the MoveData Object
        /// </summary>
        public void Save()
        {
            string OldName = MoveData.Name;// Gets the current name before saving for updateing later.

            #region Basic Info
            MoveData.Name = Basic_Name.Text;
            MoveData.Description = Basic_Desc.Text;
            #endregion

            #region Battle Details
            MoveData.Move_ActionType = (BattleManager.Data.Action_Type)Battle_ActionType.SelectedItem;
            MoveData.Move_Class = (BattleManager.Data.MoveClass)Battle_Class.SelectedItem;
            MoveData.Move_Frequency = (BattleManager.Data.Move_Frequency)Battle_Frequency.SelectedItem;
            MoveData.Move_Type = (BattleManager.Data.Type)Battle_Type.SelectedItem;

            MoveData.Move_Accuracy = (int)Battle_AC.Value;
            MoveData.Move_DamageBase = (BattleManager.Data.DamageBase)Battle_DB.Value;
            MoveData.Move_Frequency_Limit = (int)Battle_UseLimit.Value;
            #endregion

            #region Contest Details
            MoveData.Contest_Effect = (ContestManager.Data.Contest_Effects)Contest_Effect.SelectedItem;
            MoveData.Contest_Type = (ContestManager.Data.Contest_Type)Contest_Type.SelectedItem;
            #endregion

            #region Range Data
            MoveData.Range_Data = new List<VPTU.Pokedex.Moves.Move_RangeData>();

            #region All Adjacent Foes
            try
            {
                if ((bool)AllAdj_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.All_Adjacent_Foes, (bool)AllAdj_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Any Target
            try
            {
                if ((bool)AnyTarget_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Any, (bool)AnyTarget_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Burst
            try
            {
                if ((bool)Burst_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Burst, (bool)Burst_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)Burst_Size.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Cardinally Adjacent
            try
            {
                if ((bool)CardinallyAdj_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Cardinally_Adjacent, (bool)CardinallyAdj_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Close Blast
            try
            {
                if ((bool)CloseBlast_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Close_Blast, (bool)CloseBlast_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)CloseBlast_Size.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Cone
            try
            {
                if ((bool)Cone_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Cone, (bool)Cone_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)Cone_Size.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Field
            try
            {
                if ((bool)Field_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Field, (bool)Field_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Line
            try
            {
                if ((bool)Line_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Line, (bool)Line_Enabled.IsChecked, Convert.ToInt32(Decimal.Floor((decimal)Line_Size.Value)), 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Mele
            try
            {
                if ((bool)Mele_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Melee, (bool)Mele_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Range
            try
            {
                if ((bool)Range_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Range, (bool)Range_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)Range_Distance.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Ranged Blast
            try
            {
                if ((bool)RangedBlast_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Range_Blast, (bool)RangedBlast_Enabled.IsChecked, Convert.ToInt32(Decimal.Floor((decimal)RangedBlast_Size.Value)), Convert.ToInt32(Decimal.Floor((decimal)RangedBlast_Distance.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Self
            try
            {
                if ((bool)Self_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Self, (bool)Self_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #endregion

            #region Keywords
            if (MoveData.KeyWords == null)
            {
                MoveData.KeyWords = new List<KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>>();
            }
            else
            {
                MoveData.KeyWords.Clear();
            }

            if (Keyword_Aura.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Aura, true)); }
            if (Keyword_Berry.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Berry, true)); }
            if (Keyword_Blessing.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Blessing, true)); }
            if (Keyword_Coat.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Coat, true)); }
            if (Keyword_Dash.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Dash, true)); }
            if (Keyword_DubleStrike.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.DubleStrike, true)); }
            if (Keyword_Environ.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Environ, true)); }
            if (Keyword_Execute.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Execute, true)); }
            //if (Keyword_Exhaust.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Exhaust, true)); }
            if (Keyword_FiveStrike.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.FiveStrike, true)); }
            if (Keyword_Fling.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Fling, true)); }
            if (Keyword_Friendly.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Friendly, true)); }
            if (Keyword_GroundSource.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Groundsource, true)); }
            if (Keyword_Hail.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Hail, true)); }
            if (Keyword_Hazard.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Hazard, true)); }
            if (Keyword_Illusion.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Illusion, true)); }
            if (Keyword_Interupt.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Interupt, true)); }
            if (Keyword_Pass.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Pass, true)); }
            if (Keyword_Pledge.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Pledge, true)); }
            if (Keyword_Powder.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Powder, true)); }
            //if (Keyword_Priority.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Priority, (Priority)Keyword_Priority_Type.SelectedItem)); }
            if (Keyword_Push.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Push, true)); }
            if (Keyword_Rainy.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Rainy, true)); }
            if (Keyword_Reaction.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Reaction, true)); }
            //if (Keyword_Recoil.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Recoil, (Recoil)Keyword_Recoil_Type.SelectedItem)); }
            //if (Keyword_SandStorm.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.SandStorm, true)); }
            if (Keyword_Setup.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.SetUp, true)); }
            if (Keyword_Shield.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Shield, true)); }
            if (Keyword_Smite.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Smite, true)); }
            if (Keyword_Social.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Social, true)); }
            if (Keyword_Sonic.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Sonic, true)); }
            if (Keyword_SpiritSurge.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.SpiritSurge, true)); }
            if (Keyword_Sunny.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Sunny, true)); }
            if (Keyword_Trigger.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Trigger, true)); }
            if (Keyword_Vortex.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Vortex, true)); }
            if (Keyword_Weather.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.Weather, true)); }
            if (Keyword_WeightClass.IsChecked == true) { MoveData.KeyWords.Add(new KeyValuePair<VPTU.BattleManager.Data.Move_KeyWords, object>(VPTU.BattleManager.Data.Move_KeyWords.WeightClass, true)); }
            #endregion

            //Update links in different parts of the save data
            #region Update
            try
            {
                if (Update)
                {
                    if (MoveData.Name != OldName)
                    {
                        foreach (VPTU.Pokedex.Pokemon.PokemonData pokemon in SaveData.PokedexData.Pokemon)
                        {
                            if (pokemon.Moves == null)
                            {
                                pokemon.Moves = new List<VPTU.Pokedex.Pokemon.Link_Moves>();
                                continue;
                            }

                            List<VPTU.Pokedex.Pokemon.Link_Moves> moves = pokemon.Moves.FindAll(x => x.MoveName.ToLower() == OldName.ToLower());
                            if (moves != null)
                            {
                                foreach (VPTU.Pokedex.Pokemon.Link_Moves move in moves)
                                {
                                    move.MoveName = MoveData.Name;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error while updating Move Links");
                System.Windows.MessageBox.Show(ex.ToString());
            }
            #endregion
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();// Saves Data

            try { DialogResult = true; } catch { }// Sets the resault to true (Means Pass) & the error means it is not a dialogue
            Close();// Closes the dialogue
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();// Saves Data
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            try { DialogResult = false; } catch { }// Sets the resault to true (Means Fail / Canceled) & the error means it is not a dialogue
            Close();// Closes the dialogue
        }

        private void RawData_Button_Click(object sender, RoutedEventArgs e)
        {
            RAW_JSON impexp = new RAW_JSON();
            impexp.Export<VPTU.Pokedex.Moves.MoveData>(MoveData);
            bool? dr = impexp.ShowDialog();

            if (dr == true)
            {
                MoveData = impexp.Import<VPTU.Pokedex.Moves.MoveData>();
                Load();
            }
        }

        private void Effect_Designer_Click(object sender, RoutedEventArgs e)
        {
            UI.BattleEffect.BattleEffect_Designer designer = new BattleEffect.BattleEffect_Designer(MoveData.Move_Effects);
            bool? save = designer.ShowDialog();

            if (save == true)
            {
                MoveData.Move_Effects = designer.EffectData;// Save Designer
            }
        }
    }
}