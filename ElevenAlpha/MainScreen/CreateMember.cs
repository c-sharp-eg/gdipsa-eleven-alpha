﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace ElevenAlpha
{
    public partial class CreateMember : Form
    {
        ElevenAlphaEntities ctx = new ElevenAlphaEntities();
        public CreateMember()
        {
            InitializeComponent();
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            Regex isValidNumber = new Regex(@"\+\d{2}-?\d{4}-?\d{4}$");
            Regex isValidEmail = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            if (FirstNameTxtBox.Text == "")
            {
                MessageBox.Show("Please input a First Name.");
            }
           
            else if (GenderComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("Please input Gender.");
            }
            else if ((MobileTextBox.Text == "") || !isValidNumber.IsMatch(MobileTextBox.Text))
            {
                MessageBox.Show("Please input a valid Mobile number.");
            }
            
            else if (EmailTextBox.Text == "" || !isValidEmail.IsMatch(EmailTextBox.Text))
            {
                MessageBox.Show("Please input a valid Email Address.");
            }
           
            else if (EmergencyNameTextBox.Text == "")
            {
                MessageBox.Show("Please input an Emergency Contact Name.");
            }
            else if (EmergencyNumberTextBox.Text == "" || !isValidNumber.IsMatch(MobileTextBox.Text))
            {
                MessageBox.Show("Please input an Emergency Contact Number.");
            }
            else
            {

                Member newMember = new Member
                {
                    FirstName = FirstNameTxtBox.Text,
                    LastName = LNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    DateOfBirth = DOBPicker.Value,
                    Active = 1,
                    Mobile = MobileTextBox.Text,
                    EmergencyContact = EmergencyNumberTextBox.Text,
                    EmergencyName = EmergencyNameTextBox.Text,
                    EmergencyRelation = MemberRelationTextBox.Text
                };

                if (SalutationComboBox.SelectedItem == null)
                {
                    newMember.Salutations = "";
                }
                else
                {
                    newMember.Salutations = SalutationComboBox.SelectedItem.ToString();
                }

                if (GenderComboBox.SelectedItem.ToString() == "Male")
                {
                    newMember.Gender = "M";
                }
                else
                {
                    newMember.Gender = "F";
                }

                ctx.Members.Add(newMember);
                ctx.SaveChanges();


                MessageBox.Show(String.Format("New member {0} {1} with Member ID: {2} has been added!",newMember.FirstName,newMember.LastName, newMember.MemberID));

            }

           


        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
