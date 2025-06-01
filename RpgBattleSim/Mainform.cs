using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClassmatesRPGBattleSimulator
{
    // Abstract base class applying Abstraction and Encapsulation
    public abstract class StudentHero
    {
        public string Name { get; set; }
        private int health; // Encapsulated field for Health
        public int Health
        {
            get => health;
            set
            {
                health = value;
                if (health < 0) health = 0; // Prevent negative health
                if (health > MaxHealth) health = MaxHealth;
            }
        }
        public int MaxHealth { get; private set; }

        // Constructor sets name and max health
        public StudentHero(string name, int maxHealth)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        // Abstract attack method requires each subclass to implement its own attack logic (Polymorphism)
        public abstract int Attack();

        // Method to reduce health; encapsulates health modification
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    // Derived class: CamilleDebugger - specializes Attack method (Inheritance + Polymorphism)
    public class CamilleDebugger : StudentHero
    {
        private static Random rand = new Random();

        public CamilleDebugger() : base("CamilleDebugger", 100) { }

        public override int Attack()
        {
            // Debugger Camille's attack: precise and strong attack between 15-25 damage
            return rand.Next(15, 26);
        }
    }

    // Derived class: KensemiCollon - quick and agile attacks
    public class KensemiCollon : StudentHero
    {
        private static Random rand = new Random();

        public KensemiCollon() : base("KensemiCollon", 110) { }

        public override int Attack()
        {
            // Kensemi's attack: moderate damage 10-18 but chance of critical hit (double damage)
            int baseDamage = rand.Next(10, 19);
            if (rand.NextDouble() < 0.2) // 20% chance critical hit
                baseDamage *= 2;
            return baseDamage;
        }
    }

    // Derived class: LanceBackend - heavy hitter with slower but powerful strikes
    public class LanceBackend : StudentHero
    {
        private static Random rand = new Random();

        public LanceBackend() : base("LanceBackend", 120) { }

        public override int Attack()
        {
            // LanceBackend attack: slow but heavy damage 20-30 damage, 10% chance to miss (0 damage)
            if (rand.NextDouble() < 0.1)
                return 0; // Missed attack
            return rand.Next(20, 31);
        }
    }

    // Derived class: JeffPancitCanton - balanced attacker with chance to heal slightly after attack
    public class JeffPancitCanton : StudentHero
    {
        private static Random rand = new Random();

        public JeffPancitCanton() : base("JeffPancitCanton", 105) { }

        public override int Attack()
        {
            // Jeff's attack: 12-22 damage, 30% chance to heal self for 5-10 health after attack
            int damage = rand.Next(12, 23);
            if (rand.NextDouble() < 0.3)
            {
                int heal = rand.Next(5, 11);
                Health += heal; // Heal after attack
            }
            return damage;
        }
    }

    public partial class MainForm : Form
    {
        // UI controls declaration
        private Label lblPlayer1Name, lblPlayer2Name, lblPlayer1Health, lblPlayer2Health, lblWinner;
        private TextBox txtPlayer1Name, txtPlayer2Name, txtBattleLog;
        private ComboBox cbPlayer1, cbPlayer2;
        private Button btnStartBattle;
        private PictureBox pictureBoxPlayer1, pictureBoxPlayer2;

        private StudentHero player1, player2;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Classmates RPG Battle Simulator";
            this.Size = new Size(700, 670);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 30, 45); // Dark blue background

            // Player 1 Controls
            lblPlayer1Name = new Label() { Text = "Player 1 Name:", Location = new Point(30, 20), ForeColor = Color.LightBlue, AutoSize = true };
            txtPlayer1Name = new TextBox() { Location = new Point(140, 18), Width = 180, Font = new Font("Segoe UI", 10) };

            cbPlayer1 = new ComboBox() { Location = new Point(30, 50), Width = 290, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPlayer1.Items.AddRange(new string[] { "CamilleDebugger", "KensemiCollon", "LanceBackend", "JeffPancitCanton" });

            lblPlayer1Health = new Label() { Text = "Health: N/A", Location = new Point(30, 85), ForeColor = Color.LightGreen, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            pictureBoxPlayer1 = new PictureBox()
            {
                Location = new Point(140, 110),
                Size = new Size(180, 180),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Player 2 Controls
            lblPlayer2Name = new Label() { Text = "Player 2 Name:", Location = new Point(360, 20), ForeColor = Color.LightBlue, AutoSize = true };
            txtPlayer2Name = new TextBox() { Location = new Point(470, 18), Width = 180, Font = new Font("Segoe UI", 10) };

            cbPlayer2 = new ComboBox() { Location = new Point(360, 50), Width = 290, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPlayer2.Items.AddRange(new string[] { "CamilleDebugger", "KensemiCollon", "LanceBackend", "JeffPancitCanton" });

            lblPlayer2Health = new Label() { Text = "Health: N/A", Location = new Point(360, 85), ForeColor = Color.LightGreen, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            pictureBoxPlayer2 = new PictureBox()
            {
                Location = new Point(470, 110),
                Size = new Size(180, 180),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Start Battle Button
            btnStartBattle = new Button()
            {
                Text = "Start Battle",
                Location = new Point(285, 300),
                Width = 120,
                Height = 40,
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnStartBattle.FlatAppearance.BorderSize = 0;
            btnStartBattle.Click += BtnStartBattle_Click;

            // Battle Log
            txtBattleLog = new TextBox()
            {
                Location = new Point(30, 360),
                Width = 620,
                Height = 180,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new Font("Consolas", 10),
                BackColor = Color.FromArgb(15, 15, 30),
                ForeColor = Color.LightGray
            };

            // Winner Label
            lblWinner = new Label()
            {
                Text = "",
                Location = new Point(30, 550),
                ForeColor = Color.Gold,
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            // Add Controls to the form
            this.Controls.Add(lblPlayer1Name);
            this.Controls.Add(txtPlayer1Name);
            this.Controls.Add(cbPlayer1);
            this.Controls.Add(lblPlayer1Health);
            this.Controls.Add(pictureBoxPlayer1);

            this.Controls.Add(lblPlayer2Name);
            this.Controls.Add(txtPlayer2Name);
            this.Controls.Add(cbPlayer2);
            this.Controls.Add(lblPlayer2Health);
            this.Controls.Add(pictureBoxPlayer2);

            this.Controls.Add(btnStartBattle);
            this.Controls.Add(txtBattleLog);
            this.Controls.Add(lblWinner);

            // Event handlers to update images on selection
            cbPlayer1.SelectedIndexChanged += (s, e) =>
            {
                if (cbPlayer1.SelectedItem != null)
                    UpdateCharacterImage(pictureBoxPlayer1, cbPlayer1.SelectedItem.ToString());
                else
                    pictureBoxPlayer1.Image = null;
            };
            cbPlayer2.SelectedIndexChanged += (s, e) =>
            {
                if (cbPlayer2.SelectedItem != null)
                    UpdateCharacterImage(pictureBoxPlayer2, cbPlayer2.SelectedItem.ToString());
                else
                    pictureBoxPlayer2.Image = null;
            };
        }

        // Load the character images from Resources folder, disposing previous image to avoid file locks
        private void UpdateCharacterImage(PictureBox pictureBox, string characterName)
        {
            string filename = null;

            if (characterName == "CamilleDebugger")
            {
                filename = "camille.png";
            }
            else if (characterName == "KensemiCollon")
            {
                filename = "keneth.png";
            }
            else if (characterName == "LanceBackend")
            {
                filename = "lance.png";
            }
            else if (characterName == "JeffPancitCanton")
            {
                filename = "jefferson.png";
            }

            if (filename != null)
            {
                string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", filename);

                if (System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        if (pictureBox.Image != null)
                        {
                            pictureBox.Image.Dispose(); // Dispose existing image to avoid file lock
                        }

                        pictureBox.Image = Image.FromFile(imagePath);
                    }
                    catch
                    {
                        pictureBox.Image = null;
                    }
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        // Factory method to create characters based on the selection string
        private StudentHero CreateCharacter(string characterName)
        {
            switch (characterName)
            {
                case "CamilleDebugger":
                    return new CamilleDebugger();
                case "KensemiCollon":
                    return new KensemiCollon();
                case "LanceBackend":
                    return new LanceBackend();
                case "JeffPancitCanton":
                    return new JeffPancitCanton();
                default:
                    throw new ArgumentException("Invalid character selection.");
            }
        }

        // Main battle logic executed on button click
        private void BtnStartBattle_Click(object sender, EventArgs e)
        {
            // Clear previous battle state/log
            txtBattleLog.Clear();
            lblWinner.Text = "";
            lblPlayer1Health.Text = "Health: N/A";
            lblPlayer2Health.Text = "Health: N/A";

            try
            {
                // Validate player names
                string p1Name = txtPlayer1Name.Text.Trim();
                string p2Name = txtPlayer2Name.Text.Trim();
                if (string.IsNullOrWhiteSpace(p1Name))
                    throw new Exception("Player 1 name cannot be empty.");
                if (string.IsNullOrWhiteSpace(p2Name))
                    throw new Exception("Player 2 name cannot be empty.");

                // Validate character selections
                if (cbPlayer1.SelectedItem == null)
                    throw new Exception("Player 1 must select a character.");
                if (cbPlayer2.SelectedItem == null)
                    throw new Exception("Player 2 must select a character.");

                // Create character instances
                player1 = CreateCharacter(cbPlayer1.SelectedItem.ToString());
                player2 = CreateCharacter(cbPlayer2.SelectedItem.ToString());

                // Override character names with player input + character type
                player1.Name = p1Name + " (" + player1.Name + ")";
                player2.Name = p2Name + " (" + player2.Name + ")";

                // Show initial health
                UpdateHealthLabels();

                // Battle loop - players take turns attacking
                bool player1Turn = true;
                while (player1.Health > 0 && player2.Health > 0)
                {
                    if (player1Turn)
                    {
                        int damage = player1.Attack();
                        player2.TakeDamage(damage);
                        AppendBattleLog($"{player1.Name} attacks {player2.Name} and deals {damage} damage.");
                    }
                    else
                    {
                        int damage = player2.Attack();
                        player1.TakeDamage(damage);
                        AppendBattleLog($"{player2.Name} attacks {player1.Name} and deals {damage} damage.");
                    }

                    UpdateHealthLabels();
                    System.Threading.Thread.Sleep(600); // Delay for readability

                    player1Turn = !player1Turn; // Switch turns
                }

                // Determine winner and display
                string winner;
                if (player1.Health > 0)
                    winner = player1.Name;
                else if (player2.Health > 0)
                    winner = player2.Name;
                else
                    winner = "No one, it's a draw!";

                AppendBattleLog("");
                AppendBattleLog($"Battle ended! Winner: {winner}");

                lblWinner.Text = $"Winner: {winner}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Helper method to append lines to the battle log textbox with auto-scroll
        private void AppendBattleLog(string message)
        {
            txtBattleLog.AppendText(message + Environment.NewLine);
            txtBattleLog.SelectionStart = txtBattleLog.Text.Length;
            txtBattleLog.ScrollToCaret();
        }

        // Helper method to update the health labels on the UI
        private void UpdateHealthLabels()
        {
            lblPlayer1Health.Text = $"Health: {player1.Health} / {player1.MaxHealth}";
            lblPlayer2Health.Text = $"Health: {player2.Health} / {player2.MaxHealth}";
        }
    }
}
