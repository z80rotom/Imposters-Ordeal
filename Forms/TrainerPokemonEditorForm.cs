﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ImpostersOrdeal.GameDataTypes;
using static ImpostersOrdeal.GlobalData;

namespace ImpostersOrdeal
{
    public partial class TrainerPokemonEditorForm : Form
    {
        private TrainerEditorForm tef;
        private List<string> dexEntries;
        private List<string> natures;
        private List<string> abilities;
        private List<string> moves;
        private TrainerPokemon tp;

        private readonly string[] genders = new string[]
        {
            "Male", "Female", "Genderless", "Random"
        };

        public TrainerPokemonEditorForm(TrainerEditorForm tef)
        {
            this.tef = tef;
            dexEntries = gameData.dexEntries.Select(p => p.GetName()).ToList();
            natures = gameData.natures.Select(n => n.GetName()).ToList();
            abilities = gameData.abilities.Select(a => a.GetName()).ToList();
            moves = gameData.moves.Select(m => m.GetName()).ToList();

            InitializeComponent();

            speciesComboBox.DataSource = dexEntries.ToArray();
            comboBox3.DataSource = genders.ToArray();
            comboBox4.DataSource = natures.ToArray();
            comboBox5.DataSource = abilities.ToArray();
            comboBox6.DataSource = tef.items.ToArray();
            comboBox7.DataSource = moves.ToArray();
            comboBox8.DataSource = moves.ToArray();
            comboBox9.DataSource = moves.ToArray();
            comboBox10.DataSource = moves.ToArray();
        }

        public void SetTP(Trainer t, int tpIdx)
        {
            tp = t.trainerPokemon[tpIdx];
            Text = "Trainer Pokémon Editor: " + tef.trainerTypeNames[t.trainerTypeID] +
                " " + t.GetName() + " Pokémon " + (tpIdx + 1);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            speciesComboBox.SelectedIndex = tp.dexID;
            ResetFormComboBox();
            levelNumericUpDown.Value = tp.level;

            numericUpDown4.Value = tp.hpIV;
            numericUpDown5.Value = tp.atkIV;
            numericUpDown6.Value = tp.defIV;
            numericUpDown7.Value = tp.spAtkIV;
            numericUpDown8.Value = tp.spDefIV;
            numericUpDown9.Value = tp.spdIV;

            checkBox1.Checked = tp.isRare == 1;
            numericUpDown2.Value = tp.ballID;
            numericUpDown3.Value = tp.seal;

            numericUpDown15.Value = tp.hpEV;
            numericUpDown14.Value = tp.atkEV;
            numericUpDown13.Value = tp.defEV;
            numericUpDown12.Value = tp.spAtkEV;
            numericUpDown11.Value = tp.spDefEV;
            numericUpDown10.Value = tp.spdEV;

            comboBox3.SelectedIndex = tp.sex;
            comboBox4.SelectedIndex = tp.natureID;
            comboBox5.SelectedIndex = tp.abilityID;
            comboBox6.SelectedIndex = tp.itemID;

            comboBox7.SelectedIndex = tp.moveID1;
            comboBox8.SelectedIndex = tp.moveID2;
            comboBox9.SelectedIndex = tp.moveID3;
            comboBox10.SelectedIndex = tp.moveID4;

            ActivateControls();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            DeactivateControls();
        }

        private void CommitSpeciesEdit(object sender, EventArgs e)
        {
            DeactivateControls();

            tp.dexID = (ushort)(speciesComboBox.SelectedIndex == -1 ? 0 : speciesComboBox.SelectedIndex);
            tp.formID = 0;
            ResetFormComboBox();

            CommitEdit(sender, e);
            ActivateControls();
        }

        private void CommitEdit(object sender, EventArgs e)
        {
            tp.formID = (ushort)(formComboBox.SelectedIndex == -1 ? 0 : formComboBox.SelectedIndex);
            tp.level = (byte)levelNumericUpDown.Value;

            tp.hpIV = (byte)numericUpDown4.Value;
            tp.atkIV = (byte)numericUpDown4.Value;
            tp.defIV = (byte)numericUpDown4.Value;
            tp.spAtkIV = (byte)numericUpDown4.Value;
            tp.spDefIV = (byte)numericUpDown4.Value;
            tp.spdIV = (byte)numericUpDown4.Value;

            tp.isRare = (byte)(checkBox1.Checked ? 1 : 0);
            tp.ballID = (byte)numericUpDown2.Value;
            tp.seal = (int)numericUpDown3.Value;

            tp.hpEV = (byte)numericUpDown15.Value;
            tp.atkEV = (byte)numericUpDown14.Value;
            tp.defEV = (byte)numericUpDown13.Value;
            tp.spAtkEV = (byte)numericUpDown12.Value;
            tp.spDefEV = (byte)numericUpDown11.Value;
            tp.spdEV = (byte)numericUpDown10.Value;

            tp.sex = (byte)(comboBox3.SelectedIndex == -1 ? 0 : comboBox3.SelectedIndex);
            tp.natureID = (byte)(comboBox4.SelectedIndex == -1 ? 0 : comboBox4.SelectedIndex);
            tp.abilityID = (ushort)(comboBox5.SelectedIndex == -1 ? 0 : comboBox5.SelectedIndex);
            tp.itemID = (ushort)(comboBox6.SelectedIndex == -1 ? 0 : comboBox6.SelectedIndex);

            tp.moveID1 = (ushort)(comboBox7.SelectedIndex == -1 ? 0 : comboBox7.SelectedIndex);
            tp.moveID2 = (ushort)(comboBox8.SelectedIndex == -1 ? 0 : comboBox8.SelectedIndex);
            tp.moveID3 = (ushort)(comboBox9.SelectedIndex == -1 ? 0 : comboBox9.SelectedIndex);
            tp.moveID4 = (ushort)(comboBox10.SelectedIndex == -1 ? 0 : comboBox10.SelectedIndex);
        }

        private void ActivateControls()
        {
            speciesComboBox.SelectedIndexChanged += CommitSpeciesEdit;
            formComboBox.SelectedIndexChanged += CommitEdit;
            levelNumericUpDown.ValueChanged += CommitEdit;

            numericUpDown4.ValueChanged += CommitEdit;
            numericUpDown5.ValueChanged += CommitEdit;
            numericUpDown6.ValueChanged += CommitEdit;
            numericUpDown7.ValueChanged += CommitEdit;
            numericUpDown8.ValueChanged += CommitEdit;
            numericUpDown9.ValueChanged += CommitEdit;

            checkBox1.CheckedChanged += CommitEdit;
            numericUpDown2.ValueChanged += CommitEdit;
            numericUpDown3.ValueChanged += CommitEdit;

            numericUpDown15.ValueChanged += CommitEdit;
            numericUpDown14.ValueChanged += CommitEdit;
            numericUpDown13.ValueChanged += CommitEdit;
            numericUpDown12.ValueChanged += CommitEdit;
            numericUpDown11.ValueChanged += CommitEdit;
            numericUpDown10.ValueChanged += CommitEdit;

            comboBox3.SelectedIndexChanged += CommitEdit;
            comboBox4.SelectedIndexChanged += CommitEdit;
            comboBox5.SelectedIndexChanged += CommitEdit;
            comboBox6.SelectedIndexChanged += CommitEdit;

            comboBox7.SelectedIndexChanged += CommitEdit;
            comboBox8.SelectedIndexChanged += CommitEdit;
            comboBox9.SelectedIndexChanged += CommitEdit;
            comboBox10.SelectedIndexChanged += CommitEdit;
        }

        private void DeactivateControls()
        {
            speciesComboBox.SelectedIndexChanged -= CommitSpeciesEdit;
            formComboBox.SelectedIndexChanged -= CommitEdit;
            levelNumericUpDown.ValueChanged -= CommitEdit;

            numericUpDown4.ValueChanged -= CommitEdit;
            numericUpDown5.ValueChanged -= CommitEdit;
            numericUpDown6.ValueChanged -= CommitEdit;
            numericUpDown7.ValueChanged -= CommitEdit;
            numericUpDown8.ValueChanged -= CommitEdit;
            numericUpDown9.ValueChanged -= CommitEdit;

            checkBox1.CheckedChanged -= CommitEdit;
            numericUpDown2.ValueChanged -= CommitEdit;
            numericUpDown3.ValueChanged -= CommitEdit;

            numericUpDown15.ValueChanged -= CommitEdit;
            numericUpDown14.ValueChanged -= CommitEdit;
            numericUpDown13.ValueChanged -= CommitEdit;
            numericUpDown12.ValueChanged -= CommitEdit;
            numericUpDown11.ValueChanged -= CommitEdit;
            numericUpDown10.ValueChanged -= CommitEdit;

            comboBox3.SelectedIndexChanged -= CommitEdit;
            comboBox4.SelectedIndexChanged -= CommitEdit;
            comboBox5.SelectedIndexChanged -= CommitEdit;
            comboBox6.SelectedIndexChanged -= CommitEdit;

            comboBox7.SelectedIndexChanged -= CommitEdit;
            comboBox8.SelectedIndexChanged -= CommitEdit;
            comboBox9.SelectedIndexChanged -= CommitEdit;
            comboBox10.SelectedIndexChanged -= CommitEdit;
        }

        private void ResetFormComboBox()
        {
            formComboBox.SelectedIndex = -1;
            formComboBox.DataSource = gameData.dexEntries[tp.dexID].forms.Select((p, i) => i.ToString()).ToArray();
            formComboBox.SelectedIndex = tp.formID;
        }
    }
}
