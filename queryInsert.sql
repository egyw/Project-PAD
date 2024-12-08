DELETE FROM order_items;
DELETE FROM orders;
DELETE FROM payments;
DELETE FROM sales_reports;
DELETE FROM products;
DELETE FROM categories;
DELETE FROM users;
DELETE FROM discounts;

INSERT INTO categories (category_name) VALUES ('makanan'), ('minuman'), ('snack'), ('paket');

INSERT INTO products (product_name, price, DESCRIPTION, category_id) VALUES
('A&W Regular Fries', 15000.00, 'Kentang goreng renyah', 3),
('A&W Onion Rings', 18000.00, 'Cincin bawang goreng renyah', 3),
('A&W Mozzarella Sticks', 22000.00, 'Stik keju mozzarella goreng', 3),
('A&W Cheesy Fries', 18000.00, 'Kentang goreng dengan saus keju', 3),
('A&W Chicken Burger', 35000.00, 'Burger dengan ayam panggang lezat', 1),
('A&W Fish Burger', 40000.00, 'Burger dengan fillet ikan crispy', 1),
('A&W Spicy Chicken Burger', 38000.00, 'Burger ayam pedas dengan saus spesial', 1),
('A&W Beef Burger', 38000.00, 'Burger dengan daging sapi pilihan', 1),
('A&W Hotdog', 25000.00, 'Hotdog lezat dengan sosis pilihan', 1),
('A&W Chicken Nugget', 18000.00, 'Nugget ayam crispy', 3),
('A&W Double Burger', 45000.00, 'Burger ganda dengan daging sapi dan ayam', 1),
('A&W Supreme Burger', 50000.00, 'Burger premium dengan daging sapi dan ayam', 1),
('A&W Combo Meal', 65000.00, 'Paket combo burger, fries, dan minuman', 4),
('A&W Family Pack', 95000.00, 'Paket keluarga dengan burger, fries, dan minuman', 4),
('A&W Chicken Wrap', 30000.00, 'Wrap dengan ayam panggang', 1),
('A&W Eggroll', 20000.00, 'Roll dengan isi ayam dan sayuran', 3),
('A&W Root Beer', 22000.00, 'Minuman khas A&W, Root Beer', 2),
('A&W Soft Drink', 18000.00, 'Minuman soft drink pilihan', 2),
('A&W Milkshake', 24000.00, 'Milkshake rasa vanilla atau chocolate', 2),
('A&W Ice Cream Float', 25000.00, 'Es krim dicampur dengan root beer', 2),
('A&W Iced Tea', 17000.00, 'Teh manis dingin', 2),
('A&W Lemonade', 20000.00, 'Minuman lemon segar', 2),
('A&W Coffee', 22000.00, 'Kopi panas atau dingin', 2),
('A&W Sundae', 22000.00, 'Es krim sundae dengan topping', 3),
('A&W Ice Cream Cone', 18000.00, 'Es krim cone dengan topping pilihan', 3),
('A&W Chocolate Cake', 24000.00, 'Kue coklat lezat', 3),
('A&W Brownie', 22000.00, 'Brownie dengan saus coklat', 3),
('A&W Pancake', 27000.00, 'Pancake dengan sirup maple', 4),
('A&W Breakfast Set', 50000.00, 'Paket sarapan roti bakar dan minuman', 4),
('A&W Special Set', 60000.00, 'Set menu spesial dengan berbagai pilihan', 4);
