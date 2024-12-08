DELETE FROM order_items;
DELETE FROM orders;
DELETE FROM payments;
DELETE FROM sales_reports;
DELETE FROM products;
DELETE FROM categories;
DELETE FROM users;
DELETE FROM discounts;

-- Insert data into users table
INSERT INTO users (firstName, lastName, username, PASSWORD, email, phone_number, isActive) 
VALUES 
('John', 'Doe', 'johndoe', 'password123', 'john.doe@example.com', 1234567890, TRUE),
('Jane', 'Smith', 'janesmith', 'password123', 'jane.smith@example.com', 1234567891, TRUE),
('Michael', 'Johnson', 'michaelj', 'password123', 'michael.johnson@example.com', 1234567892, TRUE);

-- Insert data into categories table
INSERT INTO categories (category_name) 
VALUES 
('Beverages'),
('Snacks'),
('Main Course');

-- Insert data into products table
INSERT INTO products (product_name, price, DESCRIPTION, is_active, category_id) 
VALUES 
('Coca Cola', 1.99, 'Chilled Coca Cola 330ml', TRUE, 1),
('French Fries', 2.49, 'Crispy golden French Fries', TRUE, 2),
('Cheeseburger', 4.99, 'Classic Cheeseburger with beef patty', TRUE, 3);

-- Insert data into orders table
INSERT INTO orders (user_id, order_date, grand_total, payment_method, order_status) 
VALUES 
(1, '2024-12-01 12:00:00', 9.47, 'cash', 'completed'),
(2, '2024-12-02 14:00:00', 7.98, 'card', 'pending'),
(3, '2024-12-03 16:00:00', 12.47, 'digital_wallet', 'completed');

-- Insert data into order_items table
INSERT INTO order_items (order_id, product_id, quantity, price, total) 
VALUES 
(1, 1, 2, 1.99, 3.98),
(1, 2, 1, 2.49, 2.49),
(2, 3, 1, 4.99, 4.99);

-- Insert data into payments table
INSERT INTO payments (order_id, payment_date, amount, payment_method, payment_status) 
VALUES 
(1, '2024-12-01 12:30:00', 9.47, 'cash', 'completed'),
(2, '2024-12-02 14:30:00', 7.98, 'card', 'pending'),
(3, '2024-12-03 16:30:00', 12.47, 'digital_wallet', 'completed');

-- Insert data into inventory table
/*
INSERT INTO inventory (product_name, quantity, unit, last_updated) 
VALUES 
('Coca Cola', 100, 'pcs', '2024-12-01 12:00:00'),
('French Fries', 200, 'pcs', '2024-12-02 14:00:00'),
('Cheeseburger', 50, 'pcs', '2024-12-03 16:00:00');
*/

-- Insert data into sales_reports table
INSERT INTO sales_reports (report_date, total_sales, total_transactions) 
VALUES 
('2024-12-01', 200.00, 50),
('2024-12-02', 150.00, 40),
('2024-12-03', 300.00, 70);

-- Insert data into customers table
/*
INSERT INTO customers (NAME, email, phone_number, address) 
VALUES 
('Alice Brown', 'alice.brown@example.com', '0987654321', '123 Maple Street'),
('Bob White', 'bob.white@example.com', '0987654322', '456 Oak Avenue'),
('Charlie Green', 'charlie.green@example.com', '0987654323', '789 Pine Road');
*/

-- Insert data into reservations table
/*
INSERT INTO reservations (customer_id, reservation_date, party_size, special_request, STATUS) 
VALUES 
(1, '2024-12-05 19:00:00', 4, 'Window seat preferred', 'confirmed'),
(2, '2024-12-06 20:00:00', 2, 'No special request', 'pending'),
(3, '2024-12-07 18:00:00', 6, 'Birthday celebration', 'confirmed');
*/

-- Insert data into discounts table
INSERT INTO discounts (discount_code, DESCRIPTION, discount_percentage, start_date, end_date, is_active) 
VALUES 
('SUMMER10', '10% off for Summer promotion', 10.00, '2024-06-01', '2024-08-31', TRUE),
('HOLIDAY15', '15% off for Holiday Season', 15.00, '2024-12-01', '2024-12-31', TRUE),
('WINTER5', '5% off for Winter promotion', 5.00, '2024-12-01', '2025-02-28', TRUE);
