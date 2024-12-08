CREATE DATABASE  IF NOT EXISTS pos_aw;

USE pos_aw;

DROP TABLE IF EXISTS reservations;
DROP TABLE IF EXISTS discounts;
DROP TABLE IF EXISTS customers;
DROP TABLE IF EXISTS sales_reports;
DROP TABLE IF EXISTS inventory;
DROP TABLE IF EXISTS payments;
DROP TABLE IF EXISTS order_items;
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS categories;
DROP TABLE IF EXISTS users;

-- ini buat staff
CREATE TABLE users(
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    firstName VARCHAR(50),
    lastName VARCHAR(50),
    username VARCHAR(50) UNIQUE,
    PASSWORD VARCHAR(50),
    email VARCHAR(50),
    phone_number INT,
    isActive BOOLEAN DEFAULT TRUE
);

CREATE TABLE categories (
    category_id INT PRIMARY KEY AUTO_INCREMENT,
    category_name VARCHAR(50)
);

CREATE TABLE products (
    product_id INT PRIMARY KEY AUTO_INCREMENT,
    product_name VARCHAR(100),
    price DECIMAL(10, 2),
    DESCRIPTION TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    category_id INT, -- fk
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

CREATE TABLE orders ( -- ini kayak h_trans
    order_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT, -- fk
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    grand_total DECIMAL(10, 2),
    payment_method ENUM('cash', 'card', 'digital_wallet'),
    order_status ENUM('pending', 'completed', 'cancelled'),
    customer_name VARCHAR(50),
    order_type ENUM('dine_in', 'take_away'),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE order_items (  -- ini kayak d_trans
    order_item_id INT PRIMARY KEY AUTO_INCREMENT,
    order_id INT, -- fk
    product_id INT, -- fk
    quantity INT,
    price DECIMAL(10, 2),
    total DECIMAL(10, 2),
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE payments (
    payment_id INT PRIMARY KEY AUTO_INCREMENT,
    order_id INT,
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    amount DECIMAL(10, 2),
    payment_method ENUM('cash', 'card', 'digital_wallet'),
    payment_status ENUM('pending', 'completed', 'failed'),
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
);


CREATE TABLE sales_reports (
    report_id INT PRIMARY KEY AUTO_INCREMENT,
    report_date DATE,
    total_sales DECIMAL(10, 2),
    total_transactions INT
);

CREATE TABLE discounts (
    discount_id INT PRIMARY KEY AUTO_INCREMENT,
    discount_code VARCHAR(50) UNIQUE,
    DESCRIPTION TEXT,
    discount_percentage DECIMAL(5, 2),  
    start_date TIMESTAMP,
    end_date TIMESTAMP NULL,
    is_active BOOLEAN DEFAULT TRUE
);


-- ini ke bawah optional
/*
CREATE TABLE customers (
    customer_id INT PRIMARY KEY AUTO_INCREMENT,
    NAME VARCHAR(100),
    email VARCHAR(100),
    phone_number VARCHAR(20),
    address TEXT
);
	
CREATE TABLE reservations (
    reservation_id INT PRIMARY KEY AUTO_INCREMENT,
    customer_id INT, -- fk
    reservation_date TIMESTAMP,
    party_size INT,
    special_request TEXT,
    STATUS ENUM('pending', 'confirmed', 'cancelled'),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

CREATE TABLE inventory (
    inventory_id INT PRIMARY KEY AUTO_INCREMENT,
    product_name VARCHAR(100),
    quantity INT,
    unit VARCHAR(50), -- satuan pcs/kgs/dll
    last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
*/

	
	