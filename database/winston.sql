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
('Snacks&Sides'),
('Desserts'),
('Combo');

INSERT INTO products (product_id,product_name,price,DESCRIPTION,is_active,category_id,url_image,product_type) VALUES
(1,'ICED Hazelnut Macchiato 16',32500.00,'The perfect pick me up on a hot summer day! A rich and creamy coffee blended with aromatic notes of roasted hazelnut',1,1,'image1.jpg','minuman'),
(2,'HOT Hazelnut Macchiato 12',24500.00,'A soul warming blend of rich and creamy coffee with aromatic notes of roasted hazelnut',1,1,'image2.jpg','minuman'),
(3,'SUPER - RB Float',15500.00,'1 Reg RB Float',1,1,'image3.jpg','minuman'),
(4,'SUPER - Iced Hazelnut Macchiato',47500.00,'2 Iced Hazelnut Macchiatos',1,1,'image4.jpg','minuman'),
(5,'RB',14000.00,'A&W`s signature drink. Freshly made everyday! Taste the difference!',1,1,'image5.jpg','minuman'),
(6,'RB Float',21000.00,'Signature RB topped with creamy vanilla soft serve',1,1,'image6.jpg','minuman'),
(7,'Lemon Tea',14000.00,'Made fresh in store daily. Refreshing till the last drop!',1,1,'image7.jpg','minuman'),
(8,'Orange Juice',18000.00,'Goodness in a cup',1,1,'image8.jpg','minuman'),
(9,'Coke',14000.00,'-',1,1,'image9.jpg','minuman'),
(10,'Fanta Strawberry',14000.00,'-',1,1,'image10.jpg','minuman'),
(11,'Aqua',11500.00,'-',1,1,'image11.jpg','minuman'),
(12,'Iced Matcha Latte 16',32500.00,'Chilled. Smooth. Creamy. Matcha',1,1,'image12.jpg','minuman'),
(13,'Iced Chocolate 16',32500.00,'Take one for an instant pick-me-up!',1,1,'image13.jpg','minuman'),
(14,'Premium Hot Tea 12',16500.00,'Choice of: Green Tea, Jasmine or English Breakfast',1,1,'image14.jpg','minuman'),
(15,'Premium Hot Coffee 12',24500.00,'Premium special blend drip coffee',1,1,'image15.jpg','minuman'),
(16,'Hot Matcha Latte 12',24500.00,'Steamy. Smooth. Creamy. Matcha',1,1,'image16.jpg','minuman'),
(17,'Hot Chocolate 12',24500.00,'A balanced chocolate experience made to warm your soul',1,1,'image17.jpg','minuman'),
(18,'Japanese Curry Double Beef Burger',49500.00,'Japanese Curry Double Beef Burger',1,2,'image18.jpg','burger'),
(19,'KETO Triple Beef Burger',71000.00,'3 Beef Patties, 2 Cheese, Chicken Bacon, Tomatoes, Pickles, Lettuce and our Signature Mozza Sauce',1,2,'image19.jpg','burger'),
(20,'KETO Double Spicy Chicken Burger',75000.00,'2 Spicy Chicken Thighs, 2 Cheese, Lettuce and our unique Spicy Sauce',1,2,'image20.jpg','burger'),
(21,'Golden Aroma Chicken',24500.00,'All-time favorite. Fresh, not frozen!',1,2,'image21.jpg','ayam'),
(22,'Spicy Aroma Chicken',24500.00,'Turn up the heat!',1,2,'image22.jpg','ayam'),
(23,'Mozza Burger',46500.00,'Double beef, double-licious',1,2,'image23.jpg','burger'),
(24,'Single Deluxe Burger',39500.00,'Everything you need in a burger',1,2,'image24.jpg','burger'),
(25,'Double Cheese Burger',42500.00,'Double Cheese Burger. Double the yums!',1,2,'image25.jpg','burger'),
(26,'Cheese Burger',35500.00,'Nothing too fancy, just tasty',1,2,'image26.jpg','burger'),
(27,'Beef Burger',23500.00,'Simply delicious!',1,2,'image27.jpg','burger'),
(28,'Fish Sandwich',41500.00,'100% Fish 100% Taste!',1,2,'image28.jpg','burger'),
(29,'Jumbo Chicken Sandwich',44500.00,'You cant go wrong with classics',1,2,'image29.jpg','burger'),
(30,'Spicy Jumbo Chicken Sandwich',48500.00,'100% thigh meat marinated with our signature spicy seasoning',1,2,'image30.jpg','burger'),
(31,'Mango Chicken Pocket',34500.00,'AWesome in a pocket',1,2,'image31.jpg','none'),
(32,'Veggie Burger',46500.00,'Calling all veggie lovers',1,2,'image32.jpg','burger'),
(33,'Blackpepper Mixbowl Chicken',35500.00,'Irresistible taste. Incredible value',1,2,'image33.jpg','general'),
(34,'SUPER - 8pcs Chic Chunks',46500.00,'8pcs Chicken Chunks',1,3,'image34.jpg','snack'),
(35,'SUPER - 8pcs Durian Balls',43500.00,'8pcs Premium Durian Balls',1,3,'image35.jpg','snack'),
(36,'Rice White',13500.00,'Indonesia`s favourite!',1,3,'image36.jpg','none'),
(37,'Chicken Soup',15500.00,'The only soup you`ll ever need',1,3,'image37.jpg','soup'),
(38,'Perkedel',12000.00,'Homemade potato cake, just like mom`s',1,3,'image38.jpg','snack'),
(39,'Scrambled Eggs',17500.00,'Fluffy eggs. Perfect with everything',1,3,'image39.jpg','none'),
(40,'Sunny Egg',12000.00,'Egg-cellent choice!',1,3,'image40.jpg','none'),
(41,'Chicken Chunks',28500.00,'Tender-lovin Chicken Chunks 4pcs',1,3,'image41.jpg','snack'),
(42,'Chicken Strips (3pcs)',32500.00,'100% Premium Chicken Breast',1,3,'image42.jpg','snack'),
(43,'Curly Fries',31000.00,'A&W`s signature fries. Made of 100% USDA potatoes',1,3,'image43.jpg','snack'),
(44,'French Fries',31000.00,'Premium coated fries. Made of 100% USDA potatoes',1,3,'image44.jpg','snack'),
(45,'Duo Fries',59000.00,'Double the fun! Made of 100% USDA potatoes',1,3,'image45.jpg','snack'),
(46,'Chunks & Fries',33500.00,'Instant satisfaction!',1,3,'image46.jpg','snack'),
(47,'Chunks & Curly Fries',33500.00,'Instant satisfaction!',1,3,'image47.jpg','snack'),
(48,'Waffle & Chicken Strip',17500.00,'Great taste, great value',1,3,'image48.jpg','snack'),
(49,'Mango Dip',4500.00,'Sweet and savory!',1,3,'image49.jpg','none'),
(50,'Cheese Dip',4500.00,'Cheese makes everything taste better!',1,3,'image50.jpg','none'),
(51,'Mayo Dip',4500.00,'Creamy classic goodness!',1,3,'image51.jpg','none'),
(52,'Durian Balls',31500.00,'4pcs Durian Balls. MUST TRY!',1,3,'image52.jpg','snack'),
(53,'Waffle Regular - Ice Cream',44500.00,'Crispy on the outside, fluffy on the inside. Served with a generous dose of vanilla softserve and chocolate/strawberry sauce',1,4,'image53.jpg','icecream'),
(54,'Waffle Regular - Butter',44500.00,'Crispy on the outside, fluffy on the inside. Served with premium butter and waffle syrup',1,4,'image54.jpg','none'),
(55,'Sundae',16500.00,'Choice of: Chocolate or Strawberry topping',1,4,'image55.jpg','icecream'),
(56,'Mountain Sundae',24500.00,'Bigger is always better',1,4,'image56.jpg','icecream'),
(57,'Durian Balls Mountain Sundae',32500.00,'Extra Large Sundae with 2 Durian Balls. (Chocolate/Strawberry/Vanilla)',1,4,'image57.jpg','icecream'),
(58,'Milkshake',26500.00,'Choice of: Chocolate, Strawberry or Vanilla',1,4,'image58.jpg','none'),
(59,'Japanese Curry Chicken, Rice & RB',74500.00,'2 Japanese Curry Chicken + 1 Rice + 1 Reg RB',1,5,'image59.jpg','general'),
(60,'Japanese Curry Chicken, Fries & RB',89000.00,'2 Japanese Curry Chicken + 1 Fries (Curly/French) + 1 Reg RB',1,5,'image60.jpg','general'),
(61,'Japanese Curry Double Beef Burger & RB',57000.00,'1 Japanese Curry Double Beef Burger + 1 Reg RB',1,5,'image61.jpg','general'),
(62,'Double Cheese Burger, Fries & RB',76000.00,'1 Double Cheese Burger + Fries (Curly/French) + Reg RB',1,5,'image62.jpg','general'),
(63,'Double Cheese Burger & RB',49500.00,'1 Double Cheese Burger + Reg RB',1,5,'image63.jpg','general'),
(64,'SUPER WednesDeals - 2 Mozza Burgers',58000.00,'2 Mozza Burgers',1,5,'image64.jpg','general'),
(65,'SUPER - 2 Aroma Chicken, Chic Sandwich & Rice',88000.00,'2 Golden/Spicy Aroma Chicken + 1 Jumbo Chicken Sandwich + 1 Rice',1,5,'image65.jpg','general'),
(66,'SUPER - 2 Aroma Chicken, Cheese Burger & Fries',95000.00,'2 Golden/Spicy Aroma Chicken + 1 Cheese Burger + 1 Fries (Curly/French)',1,5,'image66.jpg','general'),
(67,'SUPER - 2 Aroma Chicken, Deluxe Burger, Rice & RB',117000.00,'2 Golden/Spicy Aroma Chicken + 1 Deluxe Burger + 2 Rice + 2 RB',1,5,'image67.jpg','general'),
(68,'SUPER - 3 Aroma Chicken, Rice, Soup & Fries',120000.00,'3 Golden/Spicy Aroma Chicken + 2 Rice + 1 Chicken Soup + 1 Fries (Curly/French)',1,5,'image68.jpg','general'),
(69,'SUPER - 4 Aroma Chicken, Double Cheese Burger & Fries',140000.00,'4 Golden/Spicy Aroma Chicken + 1 Double Cheese Burger + 1 Fries (Curly/French)',1,5,'image69.jpg','general'),
(70,'SUPER - 5 Aroma Chicken, Fries, Rice & RB',178000.00,'5 Golden/Spicy Aroma Chicken + 1 Fries (Curly/French) + 3 Rice + 2 Reg RB',1,5,'image70.jpg','general'),
(71,'SUPER - 6 Aroma Chicken, Fries, Rice & RB',197000.00,'6 Golden/Spicy Aroma Chicken + 1 Fries (Curly/French) + 3 Rice + 2 Reg RB',1,5,'image71.jpg','general'),
(72,'KETO Triple Beef Burger Combo',97000.00,'KETO Triple Beef Burger + 4pcs Chicken Chunks + 1 Aqua',1,5,'image72.jpg','general'),
(73,'KETO Double Spicy Chic Burger Combo',101000.00,'KETO Double Spicy Chicken Burger + 4pcs Chicken Chunks + 1 Aqua',1,5,'image73.jpg','general'),
(74,'KETO 2 Aroma Chicken Combo',80000.00,'2 Golden/Spicy Aroma Chicken + 4pcs Chicken Chunks + 1 Aqua',1,5,'image74.jpg','general'),
(75,'Paket Gratis 1 - Chicken',65500.00,'2 Golden/Spicy Aroma Chicken + Rice + Reg RB',1,5,'image75.jpg','general'),
(76,'Paket Gratis 2 - Chicken & Soup',53000.00,'Golden/Spicy Aroma Chicken + Chicken Soup + Rice + Reg RB',1,5,'image76.jpg','general'),
(77,'Paket Gratis 3 - Chicken Chunks & Chicken',62000.00,'4pcs Chicken Chunks + Golden/Spicy Aroma Chicken + Rice + Reg RB',1,5,'image77.jpg','general'),
(78,'Paket Gratis 4 - Deluxe Burger & Chicken',73500.00,'Deluxe Burger + Golden/Spicy Aroma Chicken + Chicken Soup + Reg RB',1,5,'image78.jpg','general'),
(79,'Paket Gratis 5 - Chicken Sandwich & Chicken',78500.00,'Chicken Sandwich + Golden/Spicy Aroma Chicken + Chicken Soup + Reg RB',1,5,'image79.jpg','general'),
(80,'Paket Gratis 6 - Blackpepper Mixbowl & Chicken',72000.00,'Blackpepper Mixbowl + Golden/Spicy Aroma Chicken + Chicken Soup + Reg RB',1,5,'image80.jpg','general'),
(81,'Paket Gratis 7 - Chicken Chunks',65500.00,'8pcs Chicken Chunks + Rice + Reg RB',1,5,'image81.jpg','general'),
(82,'Paket Kentang A - Mozza Burger',80000.00,'Mozza Burger + Fries (Curly/French) + Reg RB',1,5,'image82.jpg','general'),
(83,'Paket Kentang B - Deluxe Burger',73000.00,'eluxe Burger + Fries (Curly/French) + Reg RB',1,5,'image83.jpg','general'),
(84,'Paket Kentang C - Fish Sandwich',75000.00,'Fish Sandwich + Fries (Curly/French) + Reg RB',1,5,'image84.jpg','general'),
(85,'Paket Kentang D - Duo Aroma Chicken',80000.00,'2 Golden/Spicy Aroma Chicken + Fries (Curly/French) + Reg RB',1,5,'image85.jpg','general'),
(86,'Paket Kentang E - Chicken Sandwich',76000.00,'Chicken Sandwich + Fries (Curly/French) + Reg RB',1,5,'image86.jpg','general'),
(87,'Paket Kentang F - Mango Chicken Pocket',70000.00,'Mango Chicken Pocket + Fries (Curly/French) + Reg RB',1,5,'image87.jpg','general'),
(88,'Paket Kentang G - Chicken Chunks & Fries',84000.00,'8pcs Chicken Chunks + Fries (Curly/French) + Reg RB',1,5,'image88.jpg','general'),
(89,'HHU 1 - Aroma Chicken, Rice & Egg',40000.00,'Golden/Spicy Aroma Chicken + Rice + Scrambled Eggs',1,5,'image89.jpg','general'),
(90,'HHU 2 - Aroma Chicken, Rice, Egg & Waffle',40000.00,'Golden/Spicy Aroma Chicken + Rice + Scrambled Egg (Mini) + Waffle (Mini)',1,5,'image90.jpg','general'),
(91,'HHU 3 - Aroma Chicken, Rice & Beef',40000.00,'Golden/Spicy Aroma Chicken + Rice + Beef',1,5,'image91.jpg','general'),
(92,'Picnic Barrel 1 - Chicken & Burger',124000.00,'2 Golden/Spicy Aroma Chicken + Beef Burger + 4 Chicken Chunks™ + Rice + 2 Reg RB + Free Barrel',1,5,'image92.jpg','general'),
(93,'Picnic Barrel 2 - Chicken & Chicken Chunks™',151500.00,'4 Golden/Spicy Aroma Chicken + 4 Chicken Chunks + 2 Rice + 2 Reg RB + Free Barrel',1,5,'image93.jpg','general'),
(94,'Picnic Barrel 3 - Duo Aroma Chicken',134000.00,'6 Golden/Spicy Aroma Chicken + Free Barrel',1,5,'image94.jpg','general'),
(95,'Good Friends - Chicken & Soup',235000.00,'6 Golden/Spicy Aroma Chicken + 2 Chicken Soup + 4 Rice + 4 Reg RB',1,5,'image95.jpg','general'),
(96,'Good Family - Duo Aroma Chicken',191000.00,'9 Golden/Spicy Aroma Chicken',1,5,'image96.jpg','general');

INSERT INTO payment_method (NAME) VALUES
('cash'),
('card'),
('digital wallet');

INSERT INTO payment_details (payment_method_id, NAME) VALUES
(1, 'Cash Payment'),
(2, 'Mandiri Card'),
(2, 'BCA Card'),
(2, 'BNI Card'),
(3, 'OVO'),
(3, 'ShopeePay'),
(3, 'GoPay');

INSERT INTO orders (user_id, grand_total, order_status, customer_name, order_type) VALUES
(1, 10.50, 'completed', 'Alice Brown', 'dine_in'),
(2, 15.75, 'pending', 'Bob White', 'take_away'),
(3, 22.00, 'cancelled', 'Charlie Green', 'dine_in');

INSERT INTO order_items (order_id, product_id, quantity, price, total) VALUES
(1, 1, 1, 2.50, 2.50),
(1, 2, 2, 1.00, 2.00),
(2, 3, 1, 3.00, 3.00);

INSERT INTO payments (order_id, amount, payment_detail, payment_status) VALUES
(1, 10.50, 1, 'completed'),
(2, 15.75, 2, 'pending'),
(3, 22.00, 3, 'failed');

INSERT INTO discounts (discount_code, DESCRIPTION, discount_percentage, start_date, end_date) VALUES
('SUMMER21', 'Summer Sale Discount', 10.00, '2021-06-01', '2021-08-31'),
('WINTER21', 'Winter Sale Discount', 15.00, '2021-12-01', '2021-12-31'),
('BLACKFRIDAY', 'Black Friday Special', 20.00, '2021-11-26', '2021-11-29');


-- iki bikino ada modifier apa ae yg mungkin cth extra sauce, double meat, strawyberry icecream pokoe kabeh liaten ae di aw gofood apa ae yg mungkin
INSERT INTO modifiers (modifier_name, modifier_price) VALUES
('Golden Aroma', 0),
('Spicy Aroma', 0),
('Extra Cheese', 2000),
('Extra Sauce', 2000),
('Double Meat', 5000),
('No Cheese', 0),
('No Sauce', 0),
('No Meat', 0),
('No Vegetables', 0),
('Extra Vegetable', 1000),
('Large Size', 3000),
('Small Size', 0), -- Burger
('No Ice', 0),
('Extra Ice', 1000),
('No Float', 0),
('Extra Float', 2000), 
('Large Size', 2000), 
('Small Size', 0), -- Minuman
('Breast', 0),
('Wing', 0),
('Drumstick', 0),
('Extra Rice', 1500), -- Ayam
('Extra Strawberry Topping', 3000),
('Extra Chocolate Topping', 3000),
('No Topping', 0),
('Large Size', 2000), 
('Small Size', 1000), -- Ice Cream
('Large Size', 2000), -- Soup
('Small Size', 1000),
('Extra Sauce', 1000), -- Snack
('Golden Aroma', 0),
('Spicy Aroma', 0),
('Extra Rice', 1500),
('Extra Egg', 2000); -- general
 

-- jadi iki liaten seng product iku di field paling akhir onok type e jadi misale burger, modifier e apa ae seng cocok cocokin ama yang modifiers e 
INSERT INTO product_type_modifiers (product_type, modifier_id) VALUES
('burger', 1),  -- Golden Aroma
('burger', 2),  -- Spicy Aroma
('burger', 3),  -- Extra Cheese
('burger', 4),  -- Extra Sauce
('burger', 5),  -- Double Meat
('burger', 6),  -- No Cheese
('burger', 7),  -- No Sauce
('burger', 8),  -- No Meat
('burger', 9),  -- No Vegetables
('burger', 10), -- Extra Vegetable
('burger', 11), -- Large Size
('burger', 12), -- Small Size
('minuman', 13),  -- No Ice
('minuman', 14),  -- Extra Ice
('minuman', 15),  -- No Float
('minuman', 16),  -- Extra Float
('minuman', 17),  -- Large Size
('minuman', 18),  -- Small Size
('ayam', 19), -- Breast
('ayam', 20), -- Wing
('ayam', 21), -- Drumstick
('ayam', 22), -- Rice
('icecream', 23), -- Extra Strawberry Topping
('icecream', 24), -- Extra Chocolate Topping
('icecream', 25), -- No Topping
('icecream', 26), -- Large Size
('icecream', 27), -- Small Size
('soup', 28), -- Large Size
('soup', 29), -- Small Size
('snack', 30), -- Extra Sauce
('general', 31), -- Golden Aroma
('general', 32), -- Spicy Aroma
('general', 33), -- Extra Rice
('general', 34); -- Extra Egg


