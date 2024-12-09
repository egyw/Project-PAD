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
('Desserts');

INSERT INTO products (product_name, price, DESCRIPTION, category_id) VALUES
('Espresso', 3.50, 'A strong, brewed coffee made from finely ground beans.', 1),  -- Beverages
('Americano', 3.75, 'Espresso diluted with hot water for a lighter taste.', 1),  -- Beverages
('Cappuccino', 4.25, 'Espresso coffee topped with steamed milk and foamed milk.', 1),  -- Beverages
('Latte', 4.00, 'Espresso mixed with steamed milk and a layer of foam on top.', 1),  -- Beverages
('Mocha', 4.75, 'A coffee drink made with espresso, steamed milk, and chocolate syrup.', 1),  -- Beverages
('Macchiato', 3.25, 'Espresso with a small amount of steamed milk.', 1),  -- Beverages
('Iced Coffee', 4.00, 'Chilled coffee served with ice.', 1),  -- Beverages
('Green Tea', 2.50, 'Refreshing green tea with a light, smooth taste.', 1),  -- Beverages

('Cheeseburger', 5.00, 'A burger with a beef patty, cheese, lettuce, tomato, and pickles.', 2),  -- Food
('Chicken Burger', 5.50, 'A burger made with a grilled chicken patty and fresh toppings.', 2),  -- Food
('Veggie Burger', 5.25, 'A burger made with a plant-based patty and fresh vegetables.', 2),  -- Food
('Hot Dog', 3.00, 'A grilled sausage served in a bun with mustard and ketchup.', 2),  -- Food
('French Fries', 2.50, 'Crispy fried potatoes served with dipping sauce.', 2),  -- Food
('Onion Rings', 3.00, 'Crispy battered onion rings, served with dipping sauce.', 2),  -- Food
('Pizza Margherita', 6.00, 'A pizza topped with fresh mozzarella, tomato, and basil.', 2),  -- Food
('BBQ Chicken Pizza', 6.50, 'Pizza topped with BBQ chicken, onions, and cheese.', 2),  -- Food

('Chocolate Cake', 3.50, 'Rich and moist chocolate cake topped with creamy frosting.', 4),  -- Desserts
('Apple Pie', 3.25, 'A classic apple pie with cinnamon and sugar, served warm.', 4),  -- Desserts
('Fruit Tart', 4.00, 'A tart filled with custard and topped with fresh fruits.', 4),  -- Desserts
('Tiramisu', 5.00, 'An Italian dessert made with layers of coffee-soaked ladyfingers and mascarpone cheese.', 4),  -- Desserts
('Ice Cream Sundae', 4.25, 'Vanilla ice cream topped with chocolate syrup and whipped cream.', 4),  -- Desserts
('Brownie', 3.00, 'Fudgy and dense chocolate brownie served with vanilla ice cream.', 4),  -- Desserts
('Cheesecake', 4.50, 'Creamy cheesecake topped with fresh berries or chocolate ganache.', 4),  -- Desserts
('Pudding', 2.75, 'A smooth and creamy vanilla pudding.', 4),  -- Desserts

('Oreo Milkshake', 4.00, 'A creamy milkshake made with vanilla ice cream and crushed Oreos.', 3),  -- Snacks
('Strawberry Milkshake', 4.25, 'Milkshake made with fresh strawberries and vanilla ice cream.', 3),  -- Snacks
('Chocolate Milkshake', 4.50, 'Milkshake made with chocolate ice cream and milk.', 3),  -- Snacks
('Caramel Popcorn', 2.50, 'Popcorn drizzled with sweet caramel syrup.', 3),  -- Snacks
('Nachos', 3.00, 'Tortilla chips served with cheese, jalapenos, and salsa.', 3),  -- Snacks
('Pretzel', 2.00, 'Soft pretzel served with mustard or cheese dip.', 3),  -- Snacks
('Crisps', 1.75, 'Crispy potato chips in various flavors.', 3),  -- Snacks
('Pizza Fries', 4.00, 'Fries topped with pizza sauce, cheese, and pepperoni.', 3);  -- Snacks

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
