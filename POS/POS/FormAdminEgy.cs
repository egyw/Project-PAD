using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS
{
    public partial class FormAdminEgy : Form
    {
        int selectedIndex = -1;
        DataTable tableUsers;
        DataTable tableProducts;
        int idUser = -1;
        string selectedFilePath = string.Empty;
        string adminName = string.Empty;
        public FormAdminEgy(string name)
        {
            InitializeComponent();
            loadCBoxProducts();
            this.adminName = name;
            labelAdmin.Text = adminName;
            panelModifiers.Dock = DockStyle.Fill;
            panelProducts.Dock = DockStyle.Fill;
            panelUsers.Dock = DockStyle.Fill;
            button1.BackColor = Color.White;
            btnUpd.Text = "...";
            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);

            //users
            labelAdmin.Location = new Point(panelTop.Width - labelAdmin.Width, 18);
            label2.Location = new Point(panelTop.Width - label2.Width, 50);

            dgvUsers.Size = new Size(panelUsers.Width - dgvUsers.Location.X - 28, panelUsers.Height - dgvUsers.Location.Y - 300);
            int btnY = (dgvUsers.Height + dgvUsers.Location.Y) + 10;
            btnUpd.Location = new Point((dgvUsers.Width + dgvUsers.Location.X) - btnUpd.Width, btnY);
            btnAddUser.Location = new Point(btnUpd.Location.X - btnAddUser.Width, btnY);
            btnDelete.Location = new Point((dgvUsers.Width + dgvUsers.Location.X) - btnUpd.Width, btnY + btnUpd.Height);
            btnEditUser.Location = new Point(btnDelete.Location.X - btnEditUser.Width, btnY + btnAddUser.Height);

            groupBox1.Location = new Point(dgvUsers.Location.X, btnY);

            //products
            dgvProducts.Size = new Size(screenWidth - panelLeft.Width - dgvProducts.Location.X - 28, panelProducts.Height - dgvProducts.Location.Y - 200);
            int btnYProducts = (dgvProducts.Height + dgvProducts.Location.Y) + 10;
            groupBox2.Location = new Point(dgvProducts.Location.X, btnYProducts);
            btnEditProduct.Location = new Point((dgvProducts.Width + dgvProducts.Location.X) - btnEditProduct.Width, btnYProducts);
            btnAddProducts.Location = new Point(btnEditProduct.Location.X - btnAddProducts.Width, btnYProducts);
            btnUpdateProduct.Location = new Point((dgvProducts.Width + dgvProducts.Location.X) - btnUpdateProduct.Width, btnYProducts + btnEditProduct.Height);
            btnDeleteProduct.Location = new Point(btnUpdateProduct.Location.X - btnDeleteProduct.Width, btnYProducts + btnAddProducts.Height);

        }

        private void FormAdminEgy_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            loadDGVUsers("");
        }

        //User-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUser.Text))
            {
                loadDGVUsers("");
            }
            else
            {
                loadDGVUsers(tbUser.Text);
            }
        }

        private void loadDGVUsers(string filter)
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data;

                if (filter == "")
                {
                    data = new MySqlDataAdapter("SELECT * FROM users WHERE delete_Status = FALSE", Connection.conn);
                }
                else
                {
                    data = new MySqlDataAdapter("SELECT * FROM users WHERE firstName LIKE @fName OR lastName LIKE @lName AND delete_Status = FALSE", Connection.conn);
                    data.SelectCommand.Parameters.AddWithValue("@fName", filter + "%");
                    data.SelectCommand.Parameters.AddWithValue("@lName", filter + "%");
                }

                tableUsers = new DataTable();
                data.Fill(tableUsers);
                dgvUsers.DataSource = tableUsers;
                dgvUsers.Columns["user_id"].Visible = false;
                dgvUsers.Columns["delete_status"].Visible = false;
                dgvUsers.Columns["deleted_at"].Visible = false;

                dgvUsers.Columns["user_id"].HeaderText = "ID";
                dgvUsers.Columns["firstName"].HeaderText = "First Name";
                dgvUsers.Columns["lastName"].HeaderText = "Last Name";
                dgvUsers.Columns["PASSWORD"].HeaderText = "Password";
                dgvUsers.Columns["ROLE"].HeaderText = "Role";
                dgvUsers.Columns["phone_number"].HeaderText = "Phone Number";
                dgvUsers.Columns["isActive"].HeaderText = "Is Active";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormAddUser();
                selectedIndex = e.RowIndex;
                btnUpd.Enabled = true;
                btnDelete.Enabled = true;
                groupBox1.Enabled = false;
                try
                {
                    Connection.open();
                    idUser = Convert.ToInt32(dgvUsers.Rows[selectedIndex].Cells["user_id"].Value);
                    string status = dgvUsers.Rows[selectedIndex].Cells["isActive"].Value.ToString();
                    if(status == "True") { status = "Disable"; }
                    else { status = "Enable"; }
                    btnUpd.Text = status;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Connection.close();
                }
            }
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                bool status;
                if (btnUpd.Text == "Enable") { status = true; }
                else { status = false; }
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET isActive = @status WHERE user_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", idUser);
                cmd.ExecuteNonQuery();
                loadDGVUsers(tbUser.Text);
                reset();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void reset()
        {
            dgvUsers.ClearSelection();
            selectedIndex = -1;
            idUser = -1;
            btnDelete.Enabled = false;
            btnUpd.Enabled = false;
            btnEditUser.Enabled = false;
            btnUpd.Text = "...";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET delete_status = TRUE, deleted_at = NOW() WHERE user_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", idUser);
                cmd.ExecuteNonQuery();
                loadDGVUsers(tbUser.Text);
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            btnAdd.Enabled = true;
            resetFormAddUser();
            reset();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && comboBox1.Text != "")
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO users (firstName, lastName, username, PASSWORD, ROLE, email, phone_number) VALUES (@firstName, @lastName, @username, @password, @role, @email, @phone_number)", Connection.conn);
                    cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lastName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@username", textBox3.Text);
                    cmd.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd.Parameters.AddWithValue("@phone_number", textBox5.Text);
                    cmd.Parameters.AddWithValue("@email", textBox6.Text);
                    cmd.Parameters.AddWithValue("@role", comboBox1.Text);
                    cmd.ExecuteNonQuery();
                    loadDGVUsers(tbUser.Text);
                    resetFormAddUser();
                    reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Connection.close();
                }
            }
            else
            {
                MessageBox.Show("inputan tidak boleh kosong!");
                resetFormAddUser();
            }
        }

        private void resetFormAddUser()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditUser.Enabled = true;
            groupBox1.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpd.Enabled = false;
            try
            {
                Connection.open();
                idUser = Convert.ToInt32(dgvUsers.Rows[selectedIndex].Cells["user_id"].Value);
                textBox1.Text = dgvUsers.Rows[selectedIndex].Cells["firstName"].Value.ToString();
                textBox2.Text = dgvUsers.Rows[selectedIndex].Cells["lastName"].Value.ToString();
                textBox3.Text = dgvUsers.Rows[selectedIndex].Cells["username"].Value.ToString();
                textBox4.Text = dgvUsers.Rows[selectedIndex].Cells["PASSWORD"].Value.ToString();
                textBox5.Text = dgvUsers.Rows[selectedIndex].Cells["phone_number"].Value.ToString();
                textBox6.Text = dgvUsers.Rows[selectedIndex].Cells["email"].Value.ToString();
                comboBox1.Text = dgvUsers.Rows[selectedIndex].Cells["ROLE"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET firstName = @first, lastName = @last, username = @username, PASSWORD = @pw, phone_number = @number, email = @email, ROLE = @role WHERE user_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", idUser);
                cmd.Parameters.AddWithValue("@first", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@last", textBox2.Text.ToString());
                cmd.Parameters.AddWithValue("@username", textBox3.Text.ToString());
                cmd.Parameters.AddWithValue("@pw", textBox4.Text.ToString());
                cmd.Parameters.AddWithValue("@number", textBox5.Text.ToString());
                cmd.Parameters.AddWithValue("@email", textBox6.Text.ToString());
                cmd.Parameters.AddWithValue("@role", comboBox1.Text.ToString());

                cmd.ExecuteNonQuery();
                loadDGVUsers(tbUser.Text);
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }




        //products ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void loadDgvProducts(string filter)
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data;
                if (filter == "")
                {
                    data = new MySqlDataAdapter("SELECT p.product_id, p.product_name, p.price, p.description, c.category_name, p.product_type, p.image, p.is_active " +
                    "FROM products p " +
                    "JOIN categories c ON p.category_id = c.category_id " +
                    "WHERE delete_status = FALSE", Connection.conn);
                }
                else
                {
                    data = new MySqlDataAdapter("SELECT p.product_id, p.product_name, p.price, p.description, c.category_name, p.product_type, p.image, p.is_active " +
                    "FROM products p " +
                    "JOIN categories c ON p.category_id = c.category_id " +
                    "WHERE p.product_name LIKE @name AND delete_status = FALSE", Connection.conn);
                    data.SelectCommand.Parameters.AddWithValue("@name", filter + "%");
                }
                tableProducts = new DataTable();
                data.Fill(tableProducts);
                dgvProducts.DataSource = tableProducts;

                dgvProducts.Columns["product_id"].HeaderText = "ID";
                dgvProducts.Columns["product_name"].HeaderText = "Name";
                dgvProducts.Columns["price"].HeaderText = "Price";
                dgvProducts.Columns["description"].HeaderText = "Description";
                dgvProducts.Columns["category_name"].HeaderText = "Category";
                dgvProducts.Columns["product_type"].HeaderText = "Type";
                dgvProducts.Columns["image"].HeaderText = "Image";
                dgvProducts.Columns["is_active"].HeaderText = "Is Active";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void loadCBoxProducts()
        {
            try
            {
                Connection.open();

                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM categories", Connection.conn);
                DataTable dt = new DataTable();
                data.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "category_name";
                comboBox2.ValueMember = "category_id";

                comboBox2.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Pilih Gambar";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    textBoxImage.Text = Path.GetFileName(selectedFilePath);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Silakan pilih file gambar terlebih dahulu");
                return;
            }

            if (textBox12.Text != "" && numericUpDown1.Value != 0 && textBox9.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                string destinationFolder = Path.Combine(Application.StartupPath, "productImg");

                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }
                string selectedExtension = Path.GetExtension(selectedFilePath).ToLower();

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                if (!allowedExtensions.Contains(selectedExtension))
                {
                    MessageBox.Show("Ekstensi file tidak support. Silakan pilih file gambar dengan ekstensi .jpg, .jpeg, .png", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newFileName = GetNextFileName(destinationFolder, selectedExtension);
                string destinationPath = Path.Combine(destinationFolder, newFileName);

                try
                {
                    File.Copy(selectedFilePath, destinationPath, overwrite: false);
                    MessageBox.Show($"File berhasil disimpan sebagai {newFileName} di folder Images.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBoxImage.Text = string.Empty;
                    selectedFilePath = string.Empty;
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show($"Terjadi kesalahan saat menyimpan file: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO products (product_name, price, DESCRIPTION, category_id, image, product_type) VALUES (@name, @price, @desc, @category, @image, @type)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBox12.Text);
                    cmd.Parameters.AddWithValue("@price", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@desc", textBox9.Text);
                    cmd.Parameters.AddWithValue("@category", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("image", newFileName);
                    cmd.Parameters.AddWithValue("type", comboBox3.Text);
                    cmd.ExecuteNonQuery();
                    resetInputProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (File.Exists(destinationPath))
                    {
                        try
                        {
                            File.Delete(destinationPath);
                            MessageBox.Show("image berhasil di rollback");
                        }
                        catch (Exception deleteEx)
                        {
                            MessageBox.Show($"Terjadi kesalahan saat menghapus file setelah terjadi kesalahan: {deleteEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                finally
                {
                    Connection.close();
                }
            }
            else
            {
                MessageBox.Show("inputan tidak boleh kosong!");
            }
            resetFormAddProduct();
        }

        private string GetNextFileName(string folderPath, string extension)
        {
            string pattern = @"^image(\d+)\.\w+$";

            var files = Directory.GetFiles(folderPath, "image*.*");

            var numbers = files.Select(file =>
            {
                string fileName = Path.GetFileName(file);
                Match match = Regex.Match(fileName, pattern, RegexOptions.IgnoreCase);
                if (match.Success && int.TryParse(match.Groups[1].Value, out int number))
                {
                    return number;
                }
                return 0;
            });

            int maxNumber = numbers.Any() ? numbers.Max() : 0;

            int nextNumber = maxNumber + 1;

            string newFileName = $"image{nextNumber}{extension}";

            return newFileName;
        }

        private void resetInputProducts()
        {
            textBox12.Text = "";
            textBox9.Text = "";
            numericUpDown1.ResetText();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBoxImage.Text = "";
            selectedFilePath = string.Empty;
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCategories.Visible = false;
            selectedIndex = -1;
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCategories.Visible = true;
            selectedIndex = -1;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tbSearchProduct_TextChanged(object sender, EventArgs e)
        {
            loadDgvProducts(tbSearchProduct.Text);
        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            btnAddInBoxProduct.Enabled = true;
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE products SET product_name = @name, price = @price, DESCRIPTION = @desc, category_id = @category, product_type = @type WHERE product_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", selectedIndex);
                cmd.Parameters.AddWithValue("@name", textBox12.Text.ToString());
                cmd.Parameters.AddWithValue("@price", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@desc", textBox9.Text.ToString());
                cmd.Parameters.AddWithValue("@category", comboBox2.SelectedValue);
                cmd.Parameters.AddWithValue("@type", comboBox3.Text.ToString());

                cmd.ExecuteNonQuery();
                loadDgvProducts(tbSearchProduct.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
            resetFormAddProduct();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE products SET delete_status = TRUE, deleted_at = NOW() WHERE product_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", selectedIndex);

                cmd.ExecuteNonQuery();
                loadDgvProducts(tbSearchProduct.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
            resetFormAddProduct();
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE products SET is_active = @status WHERE product_id = @id", Connection.conn);
                bool status;
                if(btnUpdateProduct.Text == "Disable") { status = false; }
                else { status = true; }
                cmd.Parameters.AddWithValue("@id", selectedIndex);
                cmd.Parameters.AddWithValue("@status", status);

                cmd.ExecuteNonQuery();
                loadDgvProducts(tbSearchProduct.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
            resetFormAddProduct();
            dgvProducts.ClearSelection();
        }

        private void resetFormAddProduct()
        {
            btnDeleteProduct.Enabled = false;
            btnUpdateProduct.Enabled = false;
            btnEditProduct.Enabled = false;
            btnAddProducts.Enabled = true;
            textBox12.Clear();
            textBox9.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            numericUpDown1.ResetText();
            groupBox2.Enabled = false;
            btnUpdateProduct.Text = "...";
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormAddProduct();
                btnDeleteProduct.Enabled = true;
                btnUpdateProduct.Enabled = true;
                selectedIndex = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["product_id"].Value);
                if (Convert.ToBoolean(dgvProducts.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    btnUpdateProduct.Text = "Disable";
                }
                else
                {
                    btnUpdateProduct.Text = "Enable";
                }
            }

        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormAddProduct();
                groupBox2.Enabled = true;
                btnAddInBoxProduct.Enabled = false;
                btnEditProduct.Enabled = true;
                selectedIndex = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["product_id"].Value);
                textBox12.Text = dgvProducts.Rows[e.RowIndex].Cells["product_name"].Value.ToString();
                textBox9.Text = dgvProducts.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                numericUpDown1.Value = numericUpDown1.Minimum;
                numericUpDown1.Value = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["price"].Value);
                comboBox2.Text = dgvProducts.Rows[e.RowIndex].Cells["category_name"].Value.ToString();
                comboBox3.Text = dgvProducts.Rows[e.RowIndex].Cells["product_type"].Value.ToString();
            }
        }


        //general-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            resetPanel(panelUsers);
            resetChosenMenu();
            loadDGVUsers("");
            button1.BackColor = Color.White;
            panah1.Visible = true;
            panelUsers.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetPanel(panelProducts);
            resetChosenMenu();
            loadDgvProducts("");
            button2.BackColor = Color.White;
            panah2.Visible = true;
            panelProducts.Visible = true;
            dgvProducts.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button3.BackColor = Color.White;
            panah3.Visible = true;
            panelModifiers.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button4.BackColor = Color.White;
            panah4.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button5.BackColor = Color.White;
            panah5.Visible = true;
        }

        private void resetChosenMenu()
        {
            button1.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
            button5.BackColor = SystemColors.Control;

            panah1.Visible = false;
            panah2.Visible = false;
            panah3.Visible = false;
            panah4.Visible = false;
            panah5.Visible = false;

            panelUsers.Visible = false;
            panelProducts.Visible = false;
            panelModifiers.Visible = false;

            selectedIndex = -1;
        }

        private void resetPanel(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear(); 
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1; 
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.Value = numericUpDown.Minimum;
                }
                else if (control is DataGridView dataGridView)
                {
                    dataGridView.DataSource = null; 
                }
                else if (control.HasChildren)
                {
                    resetPanel(control);
                }
            }
        }

        
    }
}
