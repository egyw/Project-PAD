-- Delete all records from the tables first (reset tables)
DELETE FROM payments;
DELETE FROM order_items;
DELETE FROM orders;
DELETE FROM products;
DELETE FROM categories;
DELETE FROM payment_details;
DELETE FROM payment_method;
DELETE FROM users;
DELETE FROM discounts;

INSERT INTO users (firstName, lastName, username, PASSWORD, ROLE, email, phone_number) VALUES
('Egbert', 'Wangarry', 'eggy', '123', 'cashier', 'eggy@example.com', 1234567890),
('Given', 'Lee', 'given', '123', 'cashier', 'given@example.com', 9876543210),
('Christophani', 'Wonges', 'yoshi', '123', 'cashier', 'yoshi@example.com', 1122334455),
('Winston', 'Gatau', 'winston', '123', 'cashier', 'winston@example.com', 1122334455),
('admin', 'admin', 'admin', '123', 'admin', 'admin@example.com', 1122334455);

INSERT INTO categories (category_name) VALUES 
('Beverages'),
('Food'),
('Snacks'),
('Desserts'),
('Paket');

INSERT INTO products (product_name, price, DESCRIPTION, category_id) VALUES
-- Beverage
('Root beer', 13500, '', 1),
('Root beer float', 20500, '', 1),
('Orange Juice', 17500, '', 1),
('Lemon tea', 13500, '', 1),
('Aqua', 11000, '', 1),
('Fanta', 13500, '', 1),
('Coke', 13500, '', 1),
('Green tea latte (Ice)', 32000, '', 1),
('Green tea latte (Hot)', 24000, '', 1),
('Caramel macchiato (Ice)', 32000, '', 1),
('Caramel macchiato (Hot)', 21000, '', 1),
('Iced chocolate (Ice)', 32000, '', 1),
('Iced chocolate (Hot)', 24000, '', 1),
('Hazelnut Macchiato (Ice)', 32000, '', 1),
('Hazelnut Macchiato (Hot)', 24000, '', 1),
('Hot Tea', 16000, '', 1),
('Hot Coffee', 24000, '', 1),
('Add Float', 7000, '', 1),

-- Food
('Spicy Aroma Chicken', 24000, '', 2),
('Golden Aroma Chicken', 24000, '', 2),
('Cheese Burger', 35000, '', 2),
('Mozza Burger', 46000, '', 2),
('Beef Burger', 23000, '', 2),
('Veggie Burger', 46000, '', 2),
('Single Deluxe Burger', 37000, '', 2),
('Jumbo Chicken Sandwich', 44000, '', 2),
('Fish Sandwich', 41000, '', 2),
('Spicy Jumbo Chicken Sandwich', 47000, '', 2),
('Mango Veggie Pocket', 33000, '', 2),
('Mango Chicken Pocket', 34000, '', 2),
('Blackpepper Mixbowl Chicken', 35000, '', 2),

-- Snack
('Perkedel', 11500, '', 3),
('Chicken Soup', 15000, '', 3),
('Scrambled Eggs', 17000, '', 3),
('Chicken Strips', 32000, '', 3),
('Chicken Chunks', 28000, '', 3),
('Sunny Egg', 11500, '', 3),
('Curly Fries', 30500, '', 3),
('French Fries', 30500, '', 3),
('Duo Fries', 58000, '', 3),
('Chunks and Fries', 33000, '', 3),
('Chunks and Curly Fries', 33000, '', 3),
('Waffle & Chicken Strip', 17000, '', 3),
('Mayo/Mango/Cheese Dip', 4000, '', 3),
('Rice', 13000, '', 3),

-- Desserts
('Waffle Ice Cream', 44000, '', 4),
('Waffle Butter', 44000, '', 4),
('Durian Balls', 31000, '', 4),
('Sundae with strawberry or chocolate topping', 16000, '', 4),
('Mountain Sundae', 24000, '', 4),
('Milkshake (Strawberry/Vanilla/Chocolate)', 26000, '', 4);


INSERT INTO payment_method (NAME) 
VALUES
('cash'),
('card'),
('digital wallet');

INSERT INTO payment_details (payment_method_id, NAME) 
VALUES
(1, 'Cash Payment'),
(2, 'Mandiri Card'),
(2, 'BCA Card'),
(2, 'BNI Card'),
(3, 'OVO'),
(3, 'ShopeePay'),
(3, 'GoPay');

INSERT INTO orders (user_id, grand_total, order_status, customer_name, order_type) 
VALUES
(1, 10.50, 'completed', 'Alice Brown', 'dine_in'),
(2, 15.75, 'pending', 'Bob White', 'take_away'),
(3, 22.00, 'cancelled', 'Charlie Green', 'dine_in');

INSERT INTO order_items (order_id, product_id, quantity, price, total) 
VALUES
(1, 1, 1, 2.50, 2.50),
(1, 2, 2, 1.00, 2.00),
(2, 3, 1, 3.00, 3.00);

INSERT INTO payments (order_id, amount, payment_detail, payment_status) 
VALUES
(1, 10.50, 1, 'completed'),
(2, 15.75, 2, 'pending'),
(3, 22.00, 3, 'failed');

INSERT INTO discounts (discount_code, DESCRIPTION, discount_percentage, start_date, end_date) 
VALUES
('SUMMER21', 'Summer Sale Discount', 10.00, '2021-06-01', '2021-08-31'),
('WINTER21', 'Winter Sale Discount', 15.00, '2021-12-01', '2021-12-31'),
('BLACKFRIDAY', 'Black Friday Special', 20.00, '2021-11-26', '2021-11-29');
