-- Insert data into users table
INSERT INTO users (firstName, lastName, username, PASSWORD, ROLE, email, phone_number, isActive) 
VALUES 
('John', 'Doe', 'johndoe', 'password123', 'admin', 'john.doe@example.com', 1234567890, TRUE),
('Jane', 'Smith', 'janesmith', 'password123', 'cashier', 'jane.smith@example.com', 1234567891, TRUE),
('Michael', 'Johnson', 'michaelj', 'password123', 'manager', 'michael.johnson@example.com', 1234567892, TRUE);

-- Insert data into categories table
INSERT INTO categories (category_name, DESCRIPTION) 
VALUES 
('Beverages', 'All types of drinks including soda, coffee, and juice'),
('Snacks', 'Snacks and small eats like fries, nachos, etc.'),
('Main Course', 'Main meal options like burgers, chicken, etc.');

-- Insert data into products table
INSERT INTO products (product_name, price, DESCRIPTION, category_id) VALUES
-- Beverage 
('Root beer', 13500, 'Refreshing carbonated root beer drink', 1),
('Root beer float', 20500, 'Root beer topped with a scoop of ice cream', 1),
('Orange Juice', 17500, 'Freshly squeezed orange juice', 1),
('Lemon tea', 13500, 'Iced tea with a splash of lemon', 1),
('Aqua', 11000, 'Bottled mineral water', 1),
('Fanta', 13500, 'Fruity and fizzy orange soda', 1),
('Coke', 13500, 'Classic carbonated cola beverage', 1),
('Green tea latte (Ice)', 32000, 'Chilled green tea latte with milk', 1),
('Green tea latte (Hot)', 24000, 'Warm and creamy green tea latte', 1),
('Caramel macchiato (Ice)', 32000, 'Iced caramel-flavored espresso drink', 1),
('Caramel macchiato (Hot)', 21000, 'Hot espresso drink with caramel flavor', 1),
('Iced chocolate (Ice)', 32000, 'Cold and sweet chocolate drink', 1),
('Iced chocolate (Hot)', 24000, 'Warm and rich chocolate drink', 1),
('Hazelnut Macchiato (Ice)', 32000, 'Iced espresso with hazelnut flavor', 1),
('Hazelnut Macchiato (Hot)', 24000, 'Hot espresso with hazelnut essence', 1),
('Hot Tea', 16000, 'Classic hot brewed tea', 1),
('Hot Coffee', 24000, 'Freshly brewed black coffee', 1),
('Add Float', 7000, 'Add a scoop of ice cream to your drink', 1),

-- Food 
('Spicy Aroma Chicken', 24000, 'Crispy chicken with spicy seasoning', 2),
('Golden Aroma Chicken', 24000, 'Crispy chicken with savory golden seasoning', 2),
('Cheese Burger', 35000, 'Beef burger with melted cheese', 2),
('Mozza Burger', 46000, 'Burger topped with mozzarella cheese', 2),
('Beef Burger', 23000, 'Classic beef burger with a soft bun', 2),
('Veggie Burger', 46000, 'Vegetarian burger with fresh veggies', 2),
('Single Deluxe Burger', 37000, 'Premium burger with beef and toppings', 2),
('Jumbo Chicken Sandwich', 44000, 'Large chicken sandwich with special sauce', 2),
('Fish Sandwich', 41000, 'Breaded fish fillet in a sandwich', 2),
('Spicy Jumbo Chicken Sandwich', 47000, 'Large spicy chicken sandwich', 2),
('Mango Veggie Pocket', 33000, 'Tortilla wrap with mango and veggies', 2),
('Mango Chicken Pocket', 34000, 'Tortilla wrap with mango and chicken', 2),
('Blackpepper Mixbowl Chicken', 35000, 'Chicken rice bowl with black pepper sauce', 2),

-- Snack 
('Perkedel', 11500, 'Indonesian-style potato fritter', 3),
('Chicken Soup', 15000, 'Warm and savory chicken soup', 3),
('Scrambled Eggs', 17000, 'Fluffy scrambled eggs', 3),
('Chicken Strips', 32000, 'Crispy breaded chicken strips', 3),
('Chicken Chunks', 28000, 'Bite-sized breaded chicken pieces', 3),
('Sunny Egg', 11500, 'Fried egg with runny yolk', 3),
('Curly Fries', 30500, 'Seasoned curly-cut fries', 3),
('French Fries', 30500, 'Classic straight-cut fries', 3),
('Duo Fries', 58000, 'Combination of curly and French fries', 3),
('Chunks and Fries', 33000, 'Chicken chunks served with fries', 3),
('Chunks and Curly Fries', 33000, 'Chicken chunks served with curly fries', 3),
('Waffle & Chicken Strip', 17000, 'Waffle paired with chicken strips', 3),
('Mayo/Mango/Cheese Dip', 4000, 'Choice of mayo, mango, or cheese dip', 3),
('Rice', 13000, 'Steamed white rice', 3),

-- Desserts 
('Waffle Ice Cream', 44000, 'Waffle topped with a scoop of ice cream', 4),
('Waffle Butter', 44000, 'Classic waffle served with butter', 4),
('Durian Balls', 31000, 'Sweet durian-flavored snacks', 4),
('Sundae with strawberry or chocolate topping', 16000, 'Soft-serve sundae with topping', 4),
('Mountain Sundae', 24000, 'Sundae with layers of toppings', 4),
('Milkshake (Strawberry/Vanilla/Chocolate)', 26000, 'Milkshake with your choice of flavor', 4);

--Paket
('Paket Gratis 1', 64500, '2 Chicken + nasi + Root beer', 5),
('Paket Gratis 2', 52000, '1 Chicken + nasi + chicken soup + Root beer', 5),
('Paket Gratis 3', 61000, '4 chicken chunks + chicken + nasi + root beer', 5),
('Paket Gratis 4', 72500, '1 chicken + deluxe burger + chicken soup + root beer', 5),
('Paket Gratis 5', 77500, 'chicken sandwich + chicken + soup + root beer', 5),
('Paket Gratis 6', 69000, 'Blackpepper mixbowl + chicken + soup + root beer', 5),
('Paket Gratis 7', 64500, '8 chicken chunks + nasi + root beer', 5),
('Paket A', 78000, 'Mozza burger + fries + root beer', 5),
('Paket B', 72000, 'Deluxe burger + fries + root beer', 5),
('Paket C', 74000, 'Fish sandwich + fries + root beer', 5),
('Paket D', 79000, '2 chicken + fries + root beer', 5),
('Paket E', 75000, 'chicken sandwich + fries + root beer', 5),
('Paket F', 69000, 'mango chicken pocket + fries + root beer', 5),
('Paket G', 83000, '8 chicken chunks + fries + root beer', 5),
('Keto Triple Beef Burger + 4pcs Chicken Chunks + 1 Aqua', 96000, '', 5),
('Keto Double Spicy Chicken Burger + 4pcs Chicken Chunks + 1 Aqua', 99000, '', 5),
('Keto 2 Golden/Spicy Aroma Chicken + 4pcs Chicken Chunks + 1 Aqua', 79000, '', 5),
('Keto Triple Beef Burger', 70000, '', 5),
('Keto Double Spicy Chicken Burger', 73000, '', 5),
('HHU 1', 39500, 'Chicken + scramble egg + rice', 5),
('HHU 2', 39500, 'Chicken + scramble egg + rice + waffle', 5),
('HHU 3', 39500, 'chicken + beef + rice', 5),
('Picnic barrel 1', 120000, '2 chicken + beef burger + 4 chicken chunks + nasi + 2 root beer', 5),
('Picnic Barrel 2', 149500, '4 chicken + 2 nasi + 4 chicken chunks + 2 root beer', 5),
('Picnic Barrel 3', 132000, '6 chicken', 5),
('Good friends', 232000, '6 chicken + 2 soup + 4 nasi + 4 root beer', 5),
('Good family', 189000, '9 chicken', 5),
('Promo 1', 124000, '4 Chicken + 2 Fries + 2 RB', 5),
('Promo 2', 161000, '6 Chicken + 1 Fries + 3 Rice + 2 RB', 5),
('Promo 3', 128500, '4 Chicken + 1 Deluxe Burger + 2 Rice + 2 RB', 5),
('Promo 4', 98500, '3 Aroma Chicken + 1 Soup + 1 Fries + 2 Rice', 5),
('Promo 5', 76500, '2 Aroma Chicken + 1 Cheese Burger + 1 Fries', 5);

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
INSERT INTO inventory (product_name, quantity, unit, last_updated) 
VALUES 
('Coca Cola', 100, 'pcs', '2024-12-01 12:00:00'),
('French Fries', 200, 'pcs', '2024-12-02 14:00:00'),
('Cheeseburger', 50, 'pcs', '2024-12-03 16:00:00');

-- Insert data into sales_reports table
INSERT INTO sales_reports (report_date, total_sales, total_transactions) 
VALUES 
('2024-12-01', 200.00, 50),
('2024-12-02', 150.00, 40),
('2024-12-03', 300.00, 70);

-- Insert data into customers table
INSERT INTO customers (NAME, email, phone_number, address) 
VALUES 
('Alice Brown', 'alice.brown@example.com', '0987654321', '123 Maple Street'),
('Bob White', 'bob.white@example.com', '0987654322', '456 Oak Avenue'),
('Charlie Green', 'charlie.green@example.com', '0987654323', '789 Pine Road');

-- Insert data into reservations table
INSERT INTO reservations (customer_id, reservation_date, party_size, special_request, STATUS) 
VALUES 
(1, '2024-12-05 19:00:00', 4, 'Window seat preferred', 'confirmed'),
(2, '2024-12-06 20:00:00', 2, 'No special request', 'pending'),
(3, '2024-12-07 18:00:00', 6, 'Birthday celebration', 'confirmed');

-- Insert data into discounts table
INSERT INTO discounts (discount_code, DESCRIPTION, discount_percentage, start_date, end_date, is_active) 
VALUES 
('SUMMER10', '10% off for Summer promotion', 10.00, '2024-06-01', '2024-08-31', TRUE),
('HOLIDAY15', '15% off for Holiday Season', 15.00, '2024-12-01', '2024-12-31', TRUE),
('WINTER5', '5% off for Winter promotion', 5.00, '2024-12-01', '2025-02-28', TRUE);