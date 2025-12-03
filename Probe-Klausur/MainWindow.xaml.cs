using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;


namespace Probe_Klausur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSeminar();

        }
      

        private void LoadSeminar()
        {
            try
            {
                using (var conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM seminar";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DSeminare.ItemsSource = dt.DefaultView;
                }
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden der Seminar");
            }
        }

        public class Buchung
        {
            public int teilnehmerId { get; set; }
            public int seminarId { get; set; }
            public DateTime Buchungsdatum { get; set; }
        }

        private void BtnBuch_Click(object sender, RoutedEventArgs e)
        {
            if (DSeminare.SelectedItem == null)
            {
                msg1b1.Text = "Bitte Seminar auswählen.";
                return;
            }

            // Récupération des données sélectionnées dans le ListView
            DataRowView row = (DataRowView)DSeminare.SelectedItem;

            // Création d'un objet Buchung
            Buchung neueBuchung = new Buchung
            {
                teilnehmerId = 1, // ID du participant (fixe ici, peut être dynamique)
                seminarId = Convert.ToInt32(row["SeminarId"]),
                Buchungsdatum = DateTime.Now
            };

            // Affichage pour test/debug
            MessageBox.Show($"SeminarId={neueBuchung.seminarId}, TeilnehmerId={neueBuchung.teilnehmerId}");

            try
            {
                using (var conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO buchung (SeminarId, TeilnehmerId, Buchungsdatum) VALUES (@seminarId, @teilnehmerId, @datum)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Utilisation de l'objet Buchung pour les paramètres
                    cmd.Parameters.AddWithValue("@datum", neueBuchung.Buchungsdatum);
                    cmd.Parameters.AddWithValue("@seminarId", neueBuchung.seminarId);
                    cmd.Parameters.AddWithValue("@teilnehmerId", neueBuchung.teilnehmerId);

                    cmd.ExecuteNonQuery();
                }

                msg1b1.Text = "Buchung erfolgreich!";
            }
            catch (MySqlException ex)
            {
                msg1b1.Text = "Fehler bei der Buchung: " + ex.Message;
            }
        }
    }
}
