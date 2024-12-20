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
        DataTable tableCategories;
        DataTable tableDiscounts;
        DataTable tableModifiers;
        DataTable tableTypes;
        DataTable tableProductModifiers;
        DataTable tableMethods;
        DataTable tablePaymentDetail;
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
            panelType.Dock = DockStyle.Fill;
            panelCategories.Dock = DockStyle.Fill;
            panelProducts.Dock = DockStyle.Fill;
            panelUsers.Dock = DockStyle.Fill;
            panelDiscount.Dock = DockStyle.Fill;
            panelProductModifier.Dock = DockStyle.Fill;
            panelMethod.Dock = DockStyle.Fill;
            panelDetail.Dock = DockStyle.Fill;
            
            button1.BackColor = Color.White;
            btnUpd.Text = "...";
            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);
            int widthDGV = panelUsers.Width - dgvUsers.Location.X;
            int heightDGV = panelUsers.Height - dgvUsers.Location.Y;
            //users
            labelAdmin.Location = new Point(panelTop.Width - labelAdmin.Width, 18);
            label2.Location = new Point(panelTop.Width - label2.Width, 50);

            dgvUsers.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnY = (dgvUsers.Height + dgvUsers.Location.Y) + 10;
            btnUpd.Location = new Point((dgvUsers.Width + dgvUsers.Location.X) - btnUpd.Width, btnY);
            btnAddUser.Location = new Point(btnUpd.Location.X - btnAddUser.Width, btnY);
            btnDelete.Location = new Point((dgvUsers.Width + dgvUsers.Location.X) - btnUpd.Width, btnY + btnUpd.Height);
            btnEditUser.Location = new Point(btnDelete.Location.X - btnEditUser.Width, btnY + btnAddUser.Height);

            groupBox1.Location = new Point(dgvUsers.Location.X, btnY);

            //products
            dgvProducts.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYProducts = (dgvProducts.Height + dgvProducts.Location.Y) + 10;
            groupBox2.Location = new Point(dgvProducts.Location.X, btnYProducts);
            btnUpdateProduct.Location = new Point((dgvProducts.Width + dgvProducts.Location.X) - btnUpdateProduct.Width, btnYProducts);
            btnAddProducts.Location = new Point(btnUpdateProduct.Location.X - btnAddProducts.Width, btnYProducts);
            btnEditProduct.Location = new Point(btnAddProducts.Location.X, btnYProducts + btnAddProducts.Height);
            btnDeleteProduct.Location = new Point(btnUpdateProduct.Location.X, btnYProducts + btnUpdateProduct.Height);

            //categories
            dgvCategories.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYCategories = (dgvCategories.Height + dgvCategories.Location.Y) + 10;
            groupBox3.Location = new Point(dgvCategories.Location.X, btnYCategories);
            btnDeleteCategories.Location = new Point((dgvCategories.Width + dgvCategories.Location.X) - btnDeleteCategories.Width, btnYCategories);
            btnEditCategories.Location = new Point(btnDeleteCategories.Location.X - btnEditCategories.Width, btnYCategories);
            btnAddCategories.Location = new Point(btnEditCategories.Location.X - btnAddCategories.Width, btnYCategories);

            //type
            dgvType.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYType = (dgvType.Height + dgvType.Location.Y) + 10;
            groupBoxType.Location = new Point(dgvType.Location.X, btnYType);
            btnDeleteType.Location = new Point((dgvType.Width + dgvType.Location.X) - btnDeleteType.Width, btnYType);
            btnEditType.Location = new Point(btnDeleteType.Location.X - btnEditType.Width, btnYType);
            btnAddType.Location = new Point(btnEditType.Location.X - btnAddType.Width, btnYType);

            //discount
            dgvDiscount.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYDiscounts = (dgvDiscount.Height + dgvDiscount.Location.Y) + 10;
            groupBox4.Location = new Point(dgvDiscount.Location.X, btnYDiscounts);
            btnUpdateDiscount.Location = new Point((dgvDiscount.Width + dgvDiscount.Location.X) - btnUpdateDiscount.Width, btnYDiscounts);
            btnAddDiscount.Location = new Point(btnUpdateDiscount.Location.X - btnAddDiscount.Width, btnYDiscounts);
            btnDeleteDiscount.Location = new Point(btnUpdateDiscount.Location.X, btnYDiscounts + btnUpdateDiscount.Height);
            btnEditDiscount.Location = new Point(btnAddDiscount.Location.X, btnYDiscounts + btnAddDiscount.Height);

            //modifiers
            dgvModifiers.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYModifiers = (dgvModifiers.Height + dgvModifiers.Location.Y) + 10;
            groupBoxModifier.Location = new Point(dgvModifiers.Location.X, btnYModifiers);
            btnUpdateModifier.Location = new Point((dgvModifiers.Width + dgvModifiers.Location.X) - btnUpdateModifier.Width, btnYModifiers);
            btnAddModifier.Location = new Point(btnUpdateModifier.Location.X - btnAddModifier.Width, btnYModifiers);
            btnDeleteModifier.Location = new Point(btnUpdateModifier.Location.X, btnYModifiers + btnUpdateModifier.Height);
            btnEditModifier.Location = new Point(btnAddModifier.Location.X, btnYModifiers + btnAddModifier.Height);

            //product-modifiers
            dgvProductMod.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYProductMod = (dgvProductMod.Height + dgvProductMod.Location.Y) + 10;
            groupBoxProductMod.Location = new Point(dgvProductMod.Location.X, btnYProductMod);
            btnUpdateProductMod.Location = new Point((dgvProductMod.Width + dgvProductMod.Location.X) - btnUpdateProductMod.Width, btnYProductMod);
            btnAddProductMod.Location = new Point(btnUpdateProductMod.Location.X - btnAddProductMod.Width, btnYProductMod);
            btnDeleteProductMod.Location = new Point(btnUpdateProductMod.Location.X, btnYProductMod + btnUpdateProductMod.Height);
            btnEditProductMod.Location = new Point(btnAddProductMod.Location.X, btnYProductMod + btnAddProductMod.Height);

            //payment-method
            dgvMethod.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYMethod = (dgvMethod.Height + dgvMethod.Location.Y) + 10;
            groupBoxMethod.Location = new Point(dgvMethod.Location.X, btnYMethod);
            btnUpdateMethod.Location = new Point((dgvMethod.Width + dgvMethod.Location.X) - btnUpdateMethod.Width, btnYMethod);
            btnAddMethod.Location = new Point(btnUpdateMethod.Location.X - btnAddMethod.Width, btnYMethod);
            btnDeleteMethod.Location = new Point(btnUpdateMethod.Location.X, btnYMethod + btnUpdateMethod.Height);
            btnEditMethod.Location = new Point(btnAddMethod.Location.X, btnYMethod + btnAddMethod.Height);

            //payment-details
            dgvDetail.Size = new Size(widthDGV - 28, heightDGV - 300);
            int btnYDetail = (dgvDetail.Height + dgvDetail.Location.Y) + 10;
            groupBoxDetail.Location = new Point(dgvDetail.Location.X, btnYDetail);
            btnUpdatePayment.Location = new Point((dgvDetail.Width + dgvDetail.Location.X) - btnUpdatePayment.Width, btnYDetail);
            btnAddPayment.Location = new Point(btnUpdatePayment.Location.X - btnAddPayment.Width, btnYDetail);
            btnDeletePayment.Location = new Point(btnUpdatePayment.Location.X, btnYDetail + btnUpdatePayment.Height);
            btnEditPayment.Location = new Point(btnAddPayment.Location.X, btnYDetail + btnAddPayment.Height);
        }

        private void FormAdminEgy_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            loadDGVUsers("");
            dgvUsers.ClearSelection();
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
                    data = new MySqlDataAdapter("SELECT p.product_id, p.product_name, p.price, p.description, c.category_name, pt.type_name, p.image, p.is_active " +
                    "FROM products p " +
                    "JOIN categories c ON p.category_id = c.category_id " +
                    "JOIN product_types pt ON p.product_type = pt.product_type_id " +
                    "WHERE p.delete_status = FALSE", Connection.conn);
                }
                else
                {
                    data = new MySqlDataAdapter("SELECT p.product_id, p.product_name, p.price, p.description, c.category_name, pt.type_name, p.image, p.is_active " +
                    "FROM products p " +
                    "JOIN categories c ON p.category_id = c.category_id " +
                    "JOIN product_types pt ON p.product_type = pt.product_type_id " +
                    "WHERE p.product_name LIKE @name AND p.delete_status = FALSE", Connection.conn);
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
                dgvProducts.Columns["type_name"].HeaderText = "Type";
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

                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM categories WHERE delete_status = FALSE", Connection.conn);
                DataTable dt = new DataTable();
                data.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "category_name";
                comboBox2.ValueMember = "category_id";

                MySqlDataAdapter data2 = new MySqlDataAdapter("SELECT * FROM product_types WHERE delete_status = FALSE", Connection.conn);
                DataTable dt2 = new DataTable();
                data2.Fill(dt2);
                comboBox3.DataSource = dt2;
                comboBox3.DisplayMember = "type_name";
                comboBox3.ValueMember = "product_type_id";

                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
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
                    cmd.Parameters.AddWithValue("type", comboBox3.SelectedValue);
                    cmd.ExecuteNonQuery();
                    loadDgvProducts(tbSearchProduct.Text);
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
            resetPanel(panelProducts);
            dgvProducts.DataSource = null;
            tbSearchProduct.Text = "";
            loadDgvProducts("");
            dgvProducts.ClearSelection();
            panelCategories.Visible = false;
            panelType.Visible = false;
            selectedIndex = -1;
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanel(panelCategories);
            dgvCategories.DataSource = null;
            loadDgvCategories();
            panelCategories.Visible = true;
            panelType.Visible = false;
            selectedIndex = -1;
            dgvCategories.ClearSelection();
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanel(panelType);
            dgvType.DataSource = null;
            loadDGVType();
            panelType.Visible = true;
            panelCategories.Visible = false;
            selectedIndex = -1;
            dgvType.ClearSelection();
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
            resetFormAddProduct();
            dgvProducts.ClearSelection();
            groupBox2.Enabled = true;
            btnAddInBoxProduct.Enabled = true;
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "" && textBox12.Text != "" && numericUpDown1.Value > 0 && comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1)
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
                    cmd.Parameters.AddWithValue("@type", comboBox3.SelectedValue);

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
            else
            {
                MessageBox.Show("inputan tidak boleh kosong!");
            }
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
                comboBox3.Text = dgvProducts.Rows[e.RowIndex].Cells["type_name"].Value.ToString();
            }
        }


        //categories----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void loadDgvCategories()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM categories WHERE delete_status = FALSE", Connection.conn);

                tableCategories = new DataTable();
                data.Fill(tableCategories);
                dgvCategories.DataSource = tableCategories;

                dgvCategories.Columns["category_id"].HeaderText = "ID";
                dgvCategories.Columns["category_name"].HeaderText = "Category Name";
                    
                dgvCategories.Columns["delete_status"].Visible = false;
                dgvCategories.Columns["deleted_at"].Visible = false;
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

        private void resetFormCategories()
        {
            btnAddCategories.Enabled = true;
            btnEditCategories.Enabled = false;
            btnDeleteCategories.Enabled = false;
            btnAddCategoriesInBox.Enabled = true;
            tbNamaCategory.Clear();
            groupBox3.Enabled = false;
        }

        private void btnAddCategoriesInBox_Click(object sender, EventArgs e)
        {
            if (tbNamaCategory.Text != "")
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO categories (category_name) VALUES (@name)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbNamaCategory.Text);
                    cmd.ExecuteNonQuery();

                    loadDgvCategories();
                    loadCBoxProducts();
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
            else
            {
                MessageBox.Show("Nama kategori tidak boleh kosong!");
            }
            resetFormCategories();
        }
        private void btnAddCategories_Click(object sender, EventArgs e)
        {
            resetFormCategories();
            dgvCategories.ClearSelection();
            groupBox3.Enabled = true;
            btnAddCategoriesInBox.Enabled = true;
        }

        private void btnEditCategories_Click(object sender, EventArgs e)
        {
            if (tbNamaCategory.Text != "")
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE categories SET category_name = @name WHERE category_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbNamaCategory.Text);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDgvCategories();
                    loadCBoxProducts();
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
            else
            {
                MessageBox.Show("Nama kategori tidak boleh kosong!");
            }
            resetFormCategories();
        }

        private void btnDeleteCategories_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE categories SET delete_status = TRUE, deleted_at = NOW() WHERE category_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", selectedIndex);
                cmd.ExecuteNonQuery();

                loadDgvCategories();
                loadCBoxProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
            resetFormCategories();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormCategories();
                btnDeleteCategories.Enabled = true;

                selectedIndex = Convert.ToInt32(dgvCategories.Rows[e.RowIndex].Cells["category_id"].Value);
            }
        }

        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormCategories();
                groupBox3.Enabled = true;
                btnEditCategories.Enabled = true;
                btnAddCategoriesInBox.Enabled = false;

                selectedIndex = Convert.ToInt32(dgvCategories.Rows[e.RowIndex].Cells["category_id"].Value);
                tbNamaCategory.Text = dgvCategories.Rows[e.RowIndex].Cells["category_name"].Value.ToString();
            }
        }


        //Type ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void loadDGVType()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM product_types WHERE delete_status = FALSE", Connection.conn);

                tableTypes = new DataTable();
                data.Fill(tableTypes);
                dgvType.DataSource = tableTypes;

                dgvType.Columns["product_type_id"].HeaderText = "ID";
                dgvType.Columns["type_name"].HeaderText = "Type Name";

                dgvType.Columns["delete_status"].Visible = false;
                dgvType.Columns["deleted_at"].Visible = false;

                dgvType.ClearSelection();
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

        private void resetFormType()
        {
            tbTypeName.Clear();
            groupBoxType.Enabled = false;
            btnAddTypeInBox.Enabled = false;
            btnEditType.Enabled = false;
            btnDeleteType.Enabled = false;
            btnAddType.Enabled = true;
            selectedIndex = -1;
        }

        private void btnAddTypeInBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbTypeName.Text))
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO product_types (type_name) VALUES (@name)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbTypeName.Text);
                    cmd.ExecuteNonQuery();

                    loadDGVType();
                    resetFormType();
                    loadCBoxProducts();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            resetFormType();
            groupBoxType.Enabled = true;
            btnAddTypeInBox.Enabled = true;
            dgvType.ClearSelection();
        }

        private void btnEditType_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbTypeName.Text) && selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE product_types SET type_name = @name WHERE product_type_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbTypeName.Text);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVType();
                    resetFormType();
                    loadCBoxProducts();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE product_types SET delete_status = TRUE, deleted_at = NOW() WHERE product_type_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVType();
                    resetFormType();
                    loadCBoxProducts();
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
            else
            {
                MessageBox.Show("Tidak ada item yang dipilih!");
            }
        }

        private void dgvType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormType();
                btnDeleteType.Enabled = true;

                selectedIndex = Convert.ToInt32(dgvType.Rows[e.RowIndex].Cells["product_type_id"].Value);
            }
        }

        private void dgvType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormType();
                groupBoxType.Enabled = true;
                btnEditType.Enabled = true;
                btnAddTypeInBox.Enabled = false;

                selectedIndex = Convert.ToInt32(dgvType.Rows[e.RowIndex].Cells["product_type_id"].Value);
                tbTypeName.Text = dgvType.Rows[e.RowIndex].Cells["type_name"].Value.ToString();
            }
        }
        //discount------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void loadDGVDiscounts()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM discounts WHERE delete_status = FALSE", Connection.conn);

                tableDiscounts = new DataTable();
                data.Fill(tableDiscounts);

                dgvDiscount.DataSource = tableDiscounts;
                dgvDiscount.Columns["discount_id"].HeaderText = "ID";
                dgvDiscount.Columns["discount_code"].HeaderText = "Code";
                dgvDiscount.Columns["DESCRIPTION"].HeaderText = "Description";
                dgvDiscount.Columns["discount_percentage"].HeaderText = "Percentage";
                dgvDiscount.Columns["start_date"].HeaderText = "Start Date";
                dgvDiscount.Columns["end_date"].HeaderText = "End Date";
                dgvDiscount.Columns["is_active"].HeaderText = "Is Active";

                dgvDiscount.Columns["delete_status"].Visible = false;
                dgvDiscount.Columns["deleted_at"].Visible = false;
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

        private void resetFormDiscount()
        {
            btnDeleteDiscount.Enabled = false;
            btnEditDiscount.Enabled = false;
            btnUpdateDiscount.Enabled = false;
            btnUpdateDiscount.Text = "...";
            tbCode.Clear();
            tbDescDisc.Clear();
            numericUpDown2.Value = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            selectedIndex = -1;
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            resetFormDiscount();
            dgvDiscount.ClearSelection();
            groupBox4.Enabled = true;
            btnAddDiscountInBox.Enabled = true;
        }

        private void btnEditDiscount_Click(object sender, EventArgs e)
        {
            if (tbCode.Text != "" && numericUpDown2.Value > 0 && tbDescDisc.Text != "")
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE discounts SET discount_code = @code, DESCRIPTION = @desc, discount_percentage = @percentage, start_date = @startDate, end_date = @endDate WHERE discount_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@code", tbCode.Text);
                    cmd.Parameters.AddWithValue("@desc", tbDescDisc.Text);
                    cmd.Parameters.AddWithValue("@percentage", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);

                    cmd.ExecuteNonQuery();
                    loadDGVDiscounts();
                    resetFormDiscount();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnDeleteDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE discounts SET delete_status = TRUE, deleted_at = NOW() WHERE discount_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", selectedIndex);
                cmd.ExecuteNonQuery();

                loadDGVDiscounts();
                resetFormDiscount();
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

        private void btnUpdateDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE discounts SET is_active = @status WHERE discount_id = @id", Connection.conn);
                bool status;
                if (btnUpdateDiscount.Text == "Disable") { status = false; }
                else { status = true; }
                cmd.Parameters.AddWithValue("@id", selectedIndex);
                cmd.Parameters.AddWithValue("@status", status);

                cmd.ExecuteNonQuery();
                loadDGVDiscounts();
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

        private void btnAddDiscountInBox_Click(object sender, EventArgs e)
        {
            if (tbCode.Text != "" && numericUpDown2.Value > 0 && tbDescDisc.Text != "")
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO discounts (discount_code, DESCRIPTION, discount_percentage, start_date, end_date) VALUES (@code, @desc, @percentage, @startDate, @endDate)", Connection.conn);
                    cmd.Parameters.AddWithValue("@code", tbCode.Text);
                    cmd.Parameters.AddWithValue("@desc", tbDescDisc.Text);
                    cmd.Parameters.AddWithValue("@percentage", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);

                    cmd.ExecuteNonQuery();
                    loadDGVDiscounts();
                    resetFormDiscount();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong");
            }
        }

        private void dgvDiscount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormDiscount();
                btnDeleteDiscount.Enabled = true;
                btnUpdateDiscount.Enabled = true;
                selectedIndex = Convert.ToInt32(dgvDiscount.Rows[e.RowIndex].Cells["discount_id"].Value);

                if (Convert.ToBoolean(dgvDiscount.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    btnUpdateDiscount.Text = "Disable";
                }
                else
                {
                    btnUpdateDiscount.Text = "Enable";
                }
            }
        }

        private void dgvDiscount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormDiscount();
                groupBox4.Enabled = true;
                btnEditDiscount.Enabled = true;
                btnAddDiscountInBox.Enabled = false;

                selectedIndex = Convert.ToInt32(dgvDiscount.Rows[e.RowIndex].Cells["discount_id"].Value);
                tbCode.Text = dgvDiscount.Rows[e.RowIndex].Cells["discount_code"].Value.ToString();
                numericUpDown2.Value = numericUpDown2.Minimum;
                numericUpDown2.Value = Convert.ToInt32(dgvDiscount.Rows[e.RowIndex].Cells["discount_percentage"].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dgvDiscount.Rows[e.RowIndex].Cells["start_date"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(dgvDiscount.Rows[e.RowIndex].Cells["end_date"].Value);
                tbDescDisc.Text = dgvDiscount.Rows[e.RowIndex].Cells["DESCRIPTION"].Value.ToString();
            }
        }


        //modifiers-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void modifiersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanel(panelModifiers);
            dgvModifiers.DataSource = null;
            tbSearchModifier.Text = "";
            loadDGVModifiers(tbSearchModifier.Text);
            dgvModifiers.ClearSelection();
            panelProductModifier.Visible = false;
            selectedIndex = -1;
        }

        private void productModifiersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanel(panelProductModifier);
            dgvProductMod.DataSource = null;
            loadDGVProductModifiers(tbSearchProductMod.Text);
            loadCBoxProductMod();
            panelProductModifier.Visible = true;
            selectedIndex = -1;
            dgvProductMod.ClearSelection();
        }

        private void loadDGVModifiers(string filter)
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data;

                if (string.IsNullOrWhiteSpace(filter))
                {
                    data = new MySqlDataAdapter("SELECT * FROM modifiers WHERE delete_status = FALSE", Connection.conn);
                }
                else
                {
                    data = new MySqlDataAdapter("SELECT * FROM modifiers WHERE modifier_name LIKE @name AND delete_status = FALSE", Connection.conn);
                    data.SelectCommand.Parameters.AddWithValue("@name", filter + "%");
                }

                tableModifiers = new DataTable();
                data.Fill(tableModifiers);
                dgvModifiers.DataSource = tableModifiers;

                dgvModifiers.Columns["modifier_id"].HeaderText = "ID";
                dgvModifiers.Columns["modifier_name"].HeaderText = "Modifier Name";
                dgvModifiers.Columns["modifier_price"].HeaderText = "Price";
                dgvModifiers.Columns["is_active"].HeaderText = "Is Active";

                dgvModifiers.Columns["delete_status"].Visible = false;
                dgvModifiers.Columns["deleted_at"].Visible = false;

                dgvModifiers.ClearSelection();
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

        private void tbSearchModifier_TextChanged(object sender, EventArgs e)
        {
            loadDGVModifiers(tbSearchModifier.Text);
        }

        private void resetFormModifiers()
        {
            tbModifiers.Clear();
            numericModifier.Value = numericModifier.Minimum;
            groupBoxModifier.Enabled = false;
            btnAddModifierInBox.Enabled = false;
            btnEditModifier.Enabled = false;
            btnDeleteModifier.Enabled = false;
            btnUpdateModifier.Enabled = false;
            btnAddModifier.Enabled = true;
            selectedIndex = -1;
            btnUpdateModifier.Text = "...";
        }

        private void btnAddModifierInBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbModifiers.Text) && numericModifier.Value > 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO modifiers (modifier_name, modifier_price) VALUES (@name, @price)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbModifiers.Text);
                    cmd.Parameters.AddWithValue("@price", numericModifier.Value);
                    cmd.ExecuteNonQuery();

                    loadDGVModifiers(tbSearchModifier.Text);
                    resetFormModifiers();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnAddModifier_Click(object sender, EventArgs e)
        {
            resetFormModifiers();
            groupBoxModifier.Enabled = true;
            btnAddModifierInBox.Enabled = true;
            dgvModifiers.ClearSelection();
        }

        private void btnEditModifier_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbModifiers.Text) && numericModifier.Value > 0 && selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE modifiers SET modifier_name = @name, modifier_price = @price WHERE modifier_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbModifiers.Text);
                    cmd.Parameters.AddWithValue("@price", numericModifier.Value);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVModifiers(tbSearchModifier.Text);
                    resetFormModifiers();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnDeleteModifier_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE modifiers SET delete_status = TRUE, deleted_at = NOW() WHERE modifier_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVModifiers(tbSearchModifier.Text);
                    resetFormModifiers();
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
            else
            {
                MessageBox.Show("Tidak ada item yang dipilih!");
            }
        }

        private void btnUpdateModifier_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE modifiers SET is_active = @status WHERE modifier_id = @id", Connection.conn);
                    bool status;
                    if (btnUpdateModifier.Text == "Disable") { status = false; }
                    else { status = true; }
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();

                    loadDGVModifiers(tbSearchModifier.Text);
                    resetFormModifiers();
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

        private void dgvModifiers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormModifiers();
                selectedIndex = Convert.ToInt32(dgvModifiers.Rows[e.RowIndex].Cells["modifier_id"].Value);

                btnUpdateModifier.Enabled = true;
                btnDeleteModifier.Enabled = true;

                if (Convert.ToBoolean(dgvModifiers.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    btnUpdateModifier.Text = "Disable";
                }
                else
                {
                    btnUpdateModifier.Text = "Enable";
                }
            }
        }

        private void dgvModifiers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormModifiers();
                groupBoxModifier.Enabled = true;
                btnEditModifier.Enabled = true;
                btnAddModifierInBox.Enabled = false;

                selectedIndex = Convert.ToInt32(dgvModifiers.Rows[e.RowIndex].Cells["modifier_id"].Value);
                tbModifiers.Text = dgvModifiers.Rows[e.RowIndex].Cells["modifier_name"].Value.ToString();
                numericModifier.Value = numericModifier.Minimum;
                numericModifier.Value = Convert.ToInt32(dgvModifiers.Rows[e.RowIndex].Cells["modifier_price"].Value);
            }
        }


        //product-modifiers---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void loadDGVProductModifiers(string filter)
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data;

                if (string.IsNullOrWhiteSpace(filter))
                {
                    data = new MySqlDataAdapter(
                        "SELECT ptm.id, pt.type_name, m.modifier_name, ptm.is_active " +
                        "FROM product_type_modifiers ptm " +
                        "JOIN product_types pt ON ptm.product_type = pt.product_type_id " +
                        "JOIN modifiers m ON ptm.modifier_id = m.modifier_id " +
                        "WHERE ptm.delete_status = FALSE", Connection.conn);
                }
                else
                {
                    data = new MySqlDataAdapter(
                        "SELECT ptm.id, pt.type_name, m.modifier_name, ptm.is_active " +
                        "FROM product_type_modifiers ptm " +
                        "JOIN product_types pt ON ptm.product_type = pt.product_type_id " +
                        "JOIN modifiers m ON ptm.modifier_id = m.modifier_id " +
                        "WHERE ptm.delete_status = FALSE AND pt.type_name LIKE @filter", Connection.conn);
                    data.SelectCommand.Parameters.AddWithValue("@filter", "%" + filter + "%");
                }

                tableProductModifiers = new DataTable();
                data.Fill(tableProductModifiers);
                dgvProductMod.DataSource = tableProductModifiers;

                dgvProductMod.Columns["id"].HeaderText = "ID";
                dgvProductMod.Columns["type_name"].HeaderText = "Type";
                dgvProductMod.Columns["modifier_name"].HeaderText = "Modifier";
                dgvProductMod.Columns["is_active"].HeaderText = "Is Active";

                dgvProductMod.ClearSelection();
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

        private void loadCBoxProductMod()
        {
            try
            {
                Connection.open();

                MySqlDataAdapter data2 = new MySqlDataAdapter("SELECT * FROM product_types WHERE delete_status = FALSE", Connection.conn);
                DataTable dt2 = new DataTable();
                data2.Fill(dt2);
                cboxTypeProductMod.DataSource = dt2;
                cboxTypeProductMod.DisplayMember = "type_name";
                cboxTypeProductMod.ValueMember = "product_type_id";
               
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM modifiers WHERE delete_status = FALSE", Connection.conn);
                DataTable dt = new DataTable();
                data.Fill(dt);
                cboxModifierProductMod.DataSource = dt;
                cboxModifierProductMod.DisplayMember = "modifier_name";
                cboxModifierProductMod.ValueMember = "modifier_id";

                cboxTypeProductMod.SelectedIndex = -1;
                cboxModifierProductMod.SelectedIndex = -1;
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

        private void tbSearchProductMod_TextChanged(object sender, EventArgs e)
        {
            loadDGVProductModifiers(tbSearchProductMod.Text);
        }

        private void resetFormProductMod()
        {
            cboxTypeProductMod.SelectedIndex = -1;
            cboxModifierProductMod.SelectedIndex = -1;
            groupBoxProductMod.Enabled = false;
            btnAddProductModInBox.Enabled = false;
            btnEditProductMod.Enabled = false;
            btnDeleteProductMod.Enabled = false;
            btnUpdateProductMod.Enabled = false;
            btnAddProductMod.Enabled = true;
            selectedIndex = -1;
            btnUpdateProductMod.Text = "...";
        }

        private void btnAddProductModInBox_Click(object sender, EventArgs e)
        {
            if (cboxTypeProductMod.SelectedIndex != -1 && cboxModifierProductMod.SelectedIndex != -1)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO product_type_modifiers (product_type, modifier_id) VALUES (@type, @mod)", Connection.conn);
                    cmd.Parameters.AddWithValue("@type", cboxTypeProductMod.SelectedValue);
                    cmd.Parameters.AddWithValue("@mod", cboxModifierProductMod.SelectedValue);
                    cmd.ExecuteNonQuery();

                    loadDGVProductModifiers(tbSearchProductMod.Text);
                    resetFormProductMod();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnAddProductMod_Click(object sender, EventArgs e)
        {
            resetFormProductMod();
            groupBoxProductMod.Enabled = true;
            btnAddProductModInBox.Enabled = true;
            dgvProductMod.ClearSelection();
        }

        private void btnEditProductMod_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && cboxTypeProductMod.SelectedIndex != -1 && cboxModifierProductMod.SelectedIndex != -1)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE product_type_modifiers SET product_type = @type, modifier_id = @mod WHERE id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@type", cboxTypeProductMod.SelectedValue);
                    cmd.Parameters.AddWithValue("@mod", cboxModifierProductMod.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVProductModifiers(tbSearchProductMod.Text);
                    resetFormProductMod();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnDeleteProductMod_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE product_type_modifiers SET delete_status = TRUE, deleted_at = NOW() WHERE id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVProductModifiers(tbSearchProductMod.Text);
                    resetFormProductMod();
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
            else
            {
                MessageBox.Show("Tidak ada item yang dipilih!");
            }
        }

        private void btnUpdateProductMod_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE product_type_modifiers SET is_active = @status WHERE id = @id", Connection.conn);
                    bool status;
                    if (btnUpdateProductMod.Text == "Disable") { status = false; }
                    else { status = true; }
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();

                    loadDGVProductModifiers(tbSearchProductMod.Text);
                    resetFormProductMod();
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

        private void dgvProductMod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormProductMod();
                selectedIndex = Convert.ToInt32(dgvProductMod.Rows[e.RowIndex].Cells["id"].Value);

                btnUpdateProductMod.Enabled = true;
                btnDeleteProductMod.Enabled = true;

                if (Convert.ToBoolean(dgvProductMod.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    btnUpdateProductMod.Text = "Disable";
                }
                else
                {
                    btnUpdateProductMod.Text = "Enable";
                }
            }
        }

        private void dgvProductMod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormProductMod();
                groupBoxProductMod.Enabled = true;
                btnEditProductMod.Enabled = true;
                btnAddProductModInBox.Enabled = false;

                selectedIndex = Convert.ToInt32(dgvProductMod.Rows[e.RowIndex].Cells["id"].Value);
                cboxTypeProductMod.Text = dgvProductMod.Rows[e.RowIndex].Cells["type_name"].Value.ToString();
                cboxModifierProductMod.Text = dgvProductMod.Rows[e.RowIndex].Cells["modifier_name"].Value.ToString();
            }
        }

        //payment-method------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void methodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanel(panelMethod);
            dgvMethod.DataSource = null;
            loadDGVMethods();
            dgvMethod.ClearSelection();
            panelDetail.Visible = false;
            selectedIndex = -1;
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanel(panelDetail);
            dgvDetail.DataSource = null;
            loadDGVPaymentDetails();
            loadCBoxPaymentDetail();
            panelDetail.Visible = true;
            selectedIndex = -1;
            dgvDetail.ClearSelection();
        }

        private void loadDGVMethods()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM payment_method WHERE delete_status = FALSE", Connection.conn);

                tableMethods = new DataTable();
                data.Fill(tableMethods);
                dgvMethod.DataSource = tableMethods;

                dgvMethod.Columns["method_id"].HeaderText = "ID";
                dgvMethod.Columns["NAME"].HeaderText = "Method";
                dgvMethod.Columns["is_active"].HeaderText = "Is Active";

                dgvMethod.Columns["delete_status"].Visible = false;
                dgvMethod.Columns["deleted_at"].Visible = false;

                dgvMethod.ClearSelection();
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

        private void resetFormMethod()
        {
            tbMethod.Clear();
            groupBoxMethod.Enabled = false;
            btnAddMethodInBox.Enabled = false;
            btnEditMethod.Enabled = false;
            btnDeleteMethod.Enabled = false;
            btnUpdateMethod.Enabled = false;
            btnAddMethod.Enabled = true;
            btnUpdateMethod.Text = "...";
        }

        private void btnAddMethodInBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbMethod.Text))
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO payment_method (NAME) VALUES (@name)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbMethod.Text);
                    cmd.ExecuteNonQuery();

                    loadDGVMethods();
                    resetFormMethod();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnAddMethod_Click(object sender, EventArgs e)
        {
            resetFormMethod();
            groupBoxMethod.Enabled = true;
            btnAddMethodInBox.Enabled = true;
            dgvMethod.ClearSelection();
        }

        private void btnEditMethod_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbMethod.Text) && selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE payment_method SET NAME = @name WHERE method_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", tbMethod.Text);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVMethods();
                    resetFormMethod();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnDeleteMethod_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE payment_method SET delete_status = TRUE, deleted_at = NOW() WHERE method_id = @id", Connection.conn);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVMethods();
                    resetFormMethod();
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
            else
            {
                MessageBox.Show("Tidak ada item yang dipilih!");
            }
        }

        private void btnUpdateMethod_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE payment_method SET is_active = @status WHERE method_id = @id", Connection.conn);
                    bool status;
                    if (btnUpdateMethod.Text == "Disable") { status = false; }
                    else { status = true; }
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVMethods();
                    resetFormMethod();
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

        private void dgvMethod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormMethod();
                selectedIndex = Convert.ToInt32(dgvMethod.Rows[e.RowIndex].Cells["method_id"].Value);

                btnUpdateMethod.Enabled = true;
                btnDeleteMethod.Enabled = true;

                if (Convert.ToBoolean(dgvMethod.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    btnUpdateMethod.Text = "Disable";
                }
                else
                {
                    btnUpdateMethod.Text = "Enable";
                }
            }
        }

        private void dgvMethod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormMethod();
                groupBoxMethod.Enabled = true;
                btnEditMethod.Enabled = true;
                btnAddMethodInBox.Enabled = false;

                selectedIndex = Convert.ToInt32(dgvMethod.Rows[e.RowIndex].Cells["method_id"].Value);
                tbMethod.Text = dgvMethod.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
            }
        }


        //payment-detail------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void loadDGVPaymentDetails()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter(
                    "SELECT id, pd.NAME, pm.NAME AS method_name, pd.is_active " +
                    "FROM payment_details pd " +
                    "JOIN payment_method pm ON pd.payment_method_id = pm.method_id " +
                    "WHERE pd.delete_status = FALSE",
                    Connection.conn
                );

                tablePaymentDetail = new DataTable();
                data.Fill(tablePaymentDetail);
                dgvDetail.DataSource = tablePaymentDetail;

                dgvDetail.Columns["id"].HeaderText = "ID";
                dgvDetail.Columns["NAME"].HeaderText = "Payment Name";
                dgvDetail.Columns["method_name"].HeaderText = "Method";
                dgvDetail.Columns["is_active"].HeaderText = "Is Active";

                dgvDetail.ClearSelection();
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

        private void loadCBoxPaymentDetail()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM payment_method WHERE delete_status = FALSE", Connection.conn);
                DataTable dt = new DataTable();
                data.Fill(dt);

                comboBoxMethod.DataSource = dt;
                comboBoxMethod.DisplayMember = "NAME";
                comboBoxMethod.ValueMember = "method_id";

                comboBoxMethod.SelectedIndex = -1;
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

        private void resetFormPaymentDetail()
        {
            tbPaymentName.Clear();
            comboBoxMethod.SelectedIndex = -1;
            groupBoxDetail.Enabled = false;
            btnAddPaymentInBox.Enabled = false;
            btnEditPayment.Enabled = false;
            btnDeletePayment.Enabled = false;
            btnUpdatePayment.Enabled = false;
            btnAddPayment.Enabled = true;
            btnUpdatePayment.Text = "...";
            selectedIndex = -1;
        }

        private void btnAddPaymentInBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbPaymentName.Text) && comboBoxMethod.SelectedIndex != -1)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO payment_details (NAME, payment_method_id) VALUES (@name, @method_id)",
                        Connection.conn
                    );
                    cmd.Parameters.AddWithValue("@name", tbPaymentName.Text);
                    cmd.Parameters.AddWithValue("@method_id", comboBoxMethod.SelectedValue);
                    cmd.ExecuteNonQuery();

                    loadDGVPaymentDetails();
                    resetFormPaymentDetail();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            resetFormPaymentDetail();
            groupBoxDetail.Enabled = true;
            btnAddPaymentInBox.Enabled = true;
            dgvDetail.ClearSelection();
        }

        private void btnEditPayment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbPaymentName.Text) && comboBoxMethod.SelectedIndex != -1 && selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE payment_details SET NAME = @name, payment_method_id = @method_id WHERE id = @id",
                        Connection.conn
                    );
                    cmd.Parameters.AddWithValue("@name", tbPaymentName.Text);
                    cmd.Parameters.AddWithValue("@method_id", comboBoxMethod.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVPaymentDetails();
                    resetFormPaymentDetail();
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
            else
            {
                MessageBox.Show("Inputan tidak boleh kosong!");
            }
        }

        private void btnDeletePayment_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE payment_details SET delete_status = TRUE, deleted_at = NOW() WHERE id = @id",
                        Connection.conn
                    );
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVPaymentDetails();
                    resetFormPaymentDetail();
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
            else
            {
                MessageBox.Show("Tidak ada item yang dipilih!");
            }
        }

        private void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE payment_details SET is_active = @status WHERE id = @id",
                        Connection.conn
                    );
                    bool status;
                    if (btnUpdatePayment.Text == "Disable") { status = false; }
                    else { status = true; }
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", selectedIndex);
                    cmd.ExecuteNonQuery();

                    loadDGVPaymentDetails();
                    resetFormPaymentDetail();
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

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormPaymentDetail();
                selectedIndex = Convert.ToInt32(dgvDetail.Rows[e.RowIndex].Cells["id"].Value);

                btnUpdatePayment.Enabled = true;
                btnDeletePayment.Enabled = true;

                if (Convert.ToBoolean(dgvDetail.Rows[e.RowIndex].Cells["is_active"].Value))
                {
                    btnUpdatePayment.Text = "Disable";
                }
                else
                {
                    btnUpdatePayment.Text = "Enable";
                }
            }
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormPaymentDetail();
                groupBoxDetail.Enabled = true;
                btnEditPayment.Enabled = true;

                selectedIndex = Convert.ToInt32(dgvDetail.Rows[e.RowIndex].Cells["id"].Value);
                tbPaymentName.Text = dgvDetail.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                comboBoxMethod.Text = dgvDetail.Rows[e.RowIndex].Cells["method_name"].Value.ToString();
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
            dgvUsers.ClearSelection();
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
            resetPanel(panelModifiers);
            resetChosenMenu();
            loadDGVModifiers("");
            button3.BackColor = Color.White;
            panah3.Visible = true;
            panelModifiers.Visible = true;
            dgvModifiers.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetPanel(panelDiscount);
            resetChosenMenu();
            loadDGVDiscounts();
            button4.BackColor = Color.White;
            panah4.Visible = true;
            panelDiscount.Visible = true;
            dgvDiscount.ClearSelection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            resetPanel(panelMethod);
            resetChosenMenu();
            loadDGVMethods();
            button5.BackColor = Color.White;
            panah5.Visible = true;
            panelMethod.Visible = true;
            dgvMethod.ClearSelection();
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
            panelCategories.Visible = false;
            panelDiscount.Visible = false;
            panelProductModifier.Visible = false;
            panelMethod.Visible = false;
            panelDetail.Visible = false;

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
                else if (control is GroupBox groupBox)  
                {
                    groupBox.Enabled = false; 
                }
                else if (control.HasChildren)
                {
                    resetPanel(control);
                }
            }
        }
    }
}
