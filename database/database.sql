CREATE DATABASE IF NOT EXISTS pos_aw;
USE pos_aw;

DROP TABLE IF EXISTS order_item_modifiers;
DROP TABLE IF EXISTS product_type_modifiers;
DROP TABLE IF EXISTS modifiers;
DROP TABLE IF EXISTS payments;
DROP TABLE IF EXISTS order_items;
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS categories;
DROP TABLE IF EXISTS payment_details;
DROP TABLE IF EXISTS payment_method;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS discounts;


CREATE TABLE users(
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    firstName VARCHAR(50),
    lastName VARCHAR(50),
    username VARCHAR(50) UNIQUE,
    PASSWORD VARCHAR(50),
    ROLE ENUM('admin', 'cashier'),
    email VARCHAR(50),
    phone_number VARCHAR(20),
    isActive BOOLEAN DEFAULT TRUE,
    delete_status BOOLEAN DEFAULT FALSE,
    deleted_at TIMESTAMP NULL
);

CREATE TABLE categories (
    category_id INT PRIMARY KEY AUTO_INCREMENT,
    category_name VARCHAR(50),
    delete_status BOOLEAN DEFAULT FALSE,
    deleted_at TIMESTAMP NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY AUTO_INCREMENT,
    product_name VARCHAR(100),
    price DECIMAL(10, 2),
    DESCRIPTION TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    category_id INT,
    image VARCHAR(200),
    product_type ENUM('general', 'ayam', 'burger', 'minuman', 'snack', 'soup', 'icecream', 'none'),
    delete_status BOOLEAN DEFAULT FALSE,
    deleted_at TIMESTAMP NULL,
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

CREATE TABLE payment_method (
    method_id INT PRIMARY KEY AUTO_INCREMENT,
    NAME VARCHAR(30) NOT NULL
);

CREATE TABLE payment_details (
    id INT PRIMARY KEY AUTO_INCREMENT,
    payment_method_id INT,
    NAME VARCHAR(50) NOT NULL,
    FOREIGN KEY (payment_method_id) REFERENCES payment_method(method_id)
);

-- Tabel Orders (Header)
CREATE TABLE orders (
    order_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    grand_total DECIMAL(10, 2),
    order_status ENUM('pending', 'completed', 'cancelled'),
    customer_name VARCHAR(50),
    order_type ENUM('dine_in', 'take_away'),
    delete_status BOOLEAN DEFAULT FALSE,
    deleted_at TIMESTAMP NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Tabel Order Items (Detail)
CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY AUTO_INCREMENT,
    order_id INT,
    product_id INT,
    quantity INT,
    price DECIMAL(10, 2),
    total DECIMAL(10, 2),
    delete_status BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE payments (
    payment_id INT PRIMARY KEY AUTO_INCREMENT,
    order_id INT,
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    amount DECIMAL(10, 2),
    payment_detail INT,
    payment_status ENUM('pending', 'completed', 'failed'),
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (payment_detail) REFERENCES payment_details(id)
);

CREATE TABLE discounts (
    discount_id INT PRIMARY KEY AUTO_INCREMENT,
    discount_code VARCHAR(50) UNIQUE,
    DESCRIPTION TEXT,
    discount_percentage DECIMAL(5, 2),
    start_date TIMESTAMP,
    end_date TIMESTAMP NULL,
    is_active BOOLEAN DEFAULT TRUE,
    delete_status BOOLEAN DEFAULT FALSE,
    deleted_at TIMESTAMP NULL
);

CREATE TABLE modifiers (
    modifier_id INT PRIMARY KEY AUTO_INCREMENT,
    modifier_name VARCHAR(100) NOT NULL,
    modifier_price DECIMAL(10, 2),
    is_active BOOLEAN DEFAULT TRUE
);

-- Produk tertentu hanya memiliki modifiers tertentu
CREATE TABLE product_type_modifiers (
    id INT PRIMARY KEY AUTO_INCREMENT,
    product_type VARCHAR(50) NOT NULL,
    modifier_id INT NOT NULL,
    FOREIGN KEY (modifier_id) REFERENCES modifiers(modifier_id)
);

-- Tabel Order Item Modifiers (jika order_item memiliki modifier)
CREATE TABLE order_item_modifiers (
    order_item_modifier_id INT PRIMARY KEY AUTO_INCREMENT,
    order_item_id INT,
    modifier_id INT,
    quantity INT DEFAULT 1,
    price DECIMAL(10, 2),
    delete_status BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (order_item_id) REFERENCES order_items(order_item_id),
    FOREIGN KEY (modifier_id) REFERENCES modifiers(modifier_id)
);
