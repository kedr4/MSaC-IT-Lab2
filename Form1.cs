namespace MSaC_IT_Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Фильтр: разрешаем выбирать файлы с расширением .scala и .txt
                openFileDialog.Filter = "Scala Files (*.scala)|*.scala|Text Files (*.txt)|*.txt";
                openFileDialog.Title = "Выберите файл Scala или текстовый файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Получаем путь к выбранному файлу
                        string filePath = openFileDialog.FileName;

                        // Читаем содержимое файла
                        string fileContent = File.ReadAllText(filePath);

                        // Выводим содержимое в codeRichTextBox
                        codeRichTextBox.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        // В случае ошибки выводим сообщение
                        MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
