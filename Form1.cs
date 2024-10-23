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
                // ������: ��������� �������� ����� � ����������� .scala � .txt
                openFileDialog.Filter = "Scala Files (*.scala)|*.scala|Text Files (*.txt)|*.txt";
                openFileDialog.Title = "�������� ���� Scala ��� ��������� ����";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // �������� ���� � ���������� �����
                        string filePath = openFileDialog.FileName;

                        // ������ ���������� �����
                        string fileContent = File.ReadAllText(filePath);

                        // ������� ���������� � codeRichTextBox
                        codeRichTextBox.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        // � ������ ������ ������� ���������
                        MessageBox.Show($"������ ��� ������ �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
