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

INSERT INTO products (product_name, price, DESCRIPTION, category_id, url_image, product_type) VALUES
-- Beverage 
('ICED Hazelnut Macchiato 16', 32500, 'The perfect pick me up on a hot summer day! A rich and creamy coffee blended with aromatic notes of roasted hazelnut', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/312b9158-dfba-40d6-a086-4cbba6a998c7_menu-item-image_1725507101327.jpg?auto=format', 'minuman'),
('HOT Hazelnut Macchiato 12', 24500, 'A soul warming blend of rich and creamy coffee with aromatic notes of roasted hazelnut', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/96b86c30-6642-4a72-952d-a383a9ded0a0_menu-item-image_1725507113790.jpg?auto=format', 'minuman'),
('SUPER - RB Float', 15500, '1 Reg RB Float', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/e0426ef7-8819-4ff3-9bb9-b83eaf8c035b_menu-item-image_1732973398465.jpg?auto=format', 'minuman'),
('SUPER - Iced Hazelnut Macchiato', 47500, '2 Iced Hazelnut Macchiatos', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/b144c191-7118-42e0-97cc-c573a76fe058_menu-item-image_1732974369363.jpg?auto=format', 'minuman'),
('RB', 14000, 'A&W`s signature drink. Freshly made everyday! Taste the difference!', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/1c4b108c-44ac-464c-9e74-a29249e8449f_2735f66f-6af2-465a-832a-0e5b6cd21497.jpg?auto=format', 'minuman'),
('RB Float', 21000, 'Signature RB topped with creamy vanilla soft serve', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/70638f22-5de4-41f9-8c51-316e15e2d61b_4cba5b33-f3fe-4afd-a903-a3f6a56cc595_file20190522-6246-sbg60q.jpeg?auto=format', 'minuman'),
('Lemon Tea', 14000, 'Made fresh in store daily. Refreshing till the last drop!', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/00ffb592-1eb3-4c88-9360-116887b9dd2d_6cbe7b03-f009-4786-af53-69c40ae943b5.jpg?auto=format', 'minuman'),
('Orange Juice', 18000, 'Goodness in a cup', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/79dccb3d-9077-4a9c-935a-5bd16d57f8f2_d0918baf-5c12-48ae-803b-3e2da8f97f68.jpg?auto=format', 'minuman'),
('Coke', 14000, '-', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/efd7b375-7ee4-4f9a-8feb-1b6c1a901181_e7e9a1a4-0ea7-4ef7-b684-651f76504ab5.jpg?auto=format', 'minuman'),
('Fanta Strawberry', 14000, '-', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/3d160e11-2562-497b-b606-bc694c012eb2_52f1a756-a7b2-468e-a29f-4da3851c0f40.jpg?auto=format', 'minuman'),
('Aqua', 11500, '-', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/49d1d7b5-bb6e-47d5-9aad-17aecf091961_548f9291-e4d3-4527-8098-2eb7ecfa7574.jpg?auto=format', 'minuman'),
('Iced Matcha Latte 16', 32500, 'Chilled. Smooth. Creamy. Matcha', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/3dbc4960-52db-43fc-b361-ac97fcb4b4a0_f5a2bfef-073a-4a65-a7b0-1f1587f6a8f6.jpg?auto=format', 'minuman'),
('Iced Chocolate 16', 32500, 'Take one for an instant pick-me-up!', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/649f9915-f420-4a62-aafd-fa469ab32cc3_633a02d4-8417-4f32-b587-911c26b65dab.jpg?auto=format', 'minuman'),
('Premium Hot Tea 12', 16500, 'Choice of: Green Tea, Jasmine or English Breakfast', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/6b958c1b-b544-450d-846a-4faf1cb1e663_75c67855-5415-4261-b281-0899e9d7b116.jpg?auto=format', 'minuman'),
('Premium Hot Coffee 12', 24500, 'Premium special blend drip coffee', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/9c173509-436f-472d-9a53-8982a20b2c58_31c46f49-daaf-46ef-a17b-1f15573f161b_file20190522-6246-119i3td.jpeg?auto=format', 'minuman'),
('Hot Matcha Latte 12', 24500, 'Steamy. Smooth. Creamy. Matcha', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/958e40eb-6120-4843-a63d-eb8fecef38c0_24d1ea66-e064-4d76-acc0-51de5699ef46.jpg?auto=format', 'minuman'),
('Hot Chocolate 12', 24500, 'A balanced chocolate experience made to warm your soul', 1, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/d01c45ae-58fc-4210-b630-9b051fa28098_5887989b-b5eb-48ce-8076-c8c8e98e0d37.jpg?auto=format', 'minuman'),

-- Food 
('Japanese Curry Double Beef Burger', 49500, 'Japanese Curry Double Beef Burger', 2 ,'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ee481f9c-fdc3-416c-a650-bb5e3535e65f_menu-item-image_1731255155195.jpg?auto=format', 'burger'),
('KETO Triple Beef Burger', 71000, '3 Beef Patties, 2 Cheese, Chicken Bacon, Tomatoes, Pickles, Lettuce and our Signature Mozza Sauce', 2 ,'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ac2dcf32-4839-48bf-8916-2e546b14975d_menu-item-image_1716975040484.jpg?auto=format', 'burger'),
('KETO Double Spicy Chicken Burger', 75000, '2 Spicy Chicken Thighs, 2 Cheese, Lettuce and our unique Spicy Sauce', 2 , 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/33f8c915-fce6-4bee-b8d6-3c8b71e6c1d8_menu-item-image_1716975043629.jpg?auto=format', 'burger'),
('Golden Aroma Chicken', 24500, 'All-time favorite. Fresh, not frozen!', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/a786a99d-f76a-41a3-8ffe-c40aa9958e85_e1dc74e7-60b0-40a6-85a3-77b206dca540.jpg?auto=format', 'ayam'),
('Spicy Aroma Chicken', 24500, 'Turn up the heat!', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/2c7707fd-0074-45ef-aa1f-f63ac3dff1ee_d97999a7-2385-4abd-8019-70f506ca17cf.jpg?auto=format', 'ayam'),
('Mozza Burger', 46500, 'Double beef, double-licious', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/b331e154-b9bc-402c-a6a8-483b79fad0f7_menu-item-image_1706770756376.jpg?auto=format', 'burger'),
('Single Deluxe Burger', 39500, 'Everything you need in a burger', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/5028bb1e-5f0e-4b13-810b-d9db9cba3ffd_menu-item-image_1706770759723.jpg?auto=format', 'burger'),
('Double Cheese Burger', 42500, 'Double Cheese Burger. Double the yums!', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/d5c6eea0-6983-4f42-977c-4d9b184c9bd8_menu-item-image_1726546236970.jpg?auto=format', 'burger'),
('Cheese Burger', 35500, 'Nothing too fancy, just tasty', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/87fce0e4-216b-41a9-a4b4-c3bcb6ae3260_menu-item-image_1706770743132.jpg?auto=format', 'burger'),
('Beef Burger', 23500, 'Simply delicious!', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/bfbd533c-6544-40ff-a7f8-31f618eb1355_menu-item-image_1706770737964.jpg?auto=format', 'burger'),
('Fish Sandwich', 41500, '100% Fish 100% Taste!', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ffa2fae0-abe9-4d0c-a1b3-0f298950d264_menu-item-image_1706770746508.jpg?auto=format', 'burger'),
('Jumbo Chicken Sandwich', 44500, 'You cant go wrong with classics', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/3133fb13-f6de-493d-b8b9-c5d100cb1343_menu-item-image_1706770749485.jpg?auto=format', 'burger'),
('Spicy Jumbo Chicken Sandwich', 48500, '100% thigh meat marinated with our signature spicy seasoning', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/38aca071-6f95-4e23-a61f-3d4510d0fd76_menu-item-image_1706770732567.jpg?auto=format', 'burger'),
('Mango Chicken Pocket', 34500, 'AWesome in a pocket', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/4ec2b42f-e7b0-4657-a138-ff64c7f59f90_menu-item-image_1706770753046.jpg?auto=format', 'none'),
('Veggie Burger', 46500, 'Calling all veggie lovers', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/3ee82dd6-426e-4bee-8e37-76f9334ba2f2_menu-item-image_1706770762869.jpg?auto=format', 'burger'),
('Blackpepper Mixbowl Chicken', 35500, 'Irresistible taste. Incredible value', 2, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ffa3a6d6-2376-4379-8ad9-3f3c4305935d_menu-item-image_1657600458317.jpg?auto=format', 'general'),

-- Snack & Sides
('SUPER - 8pcs Chic Chunks', 46500, '8pcs Chicken Chunks', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/2c24dd74-5f16-4521-8cbb-dfd054f21c46_menu-item-image_1732974372986.jpg?auto=format', 'snack'),
('SUPER - 8pcs Durian Balls', 43500, '8pcs Premium Durian Balls', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/f0faf619-1aae-4f24-a4f4-b1a16348adf3_menu-item-image_1732974375066.jpg?auto=format', 'snack'),
('Rice White', 13500, 'Indonesia`s favourite!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/3a9f05f4-993f-4243-8eb8-813b37079186_d4ab247e-5b07-4f46-869c-ad09b5c42fd9.jpg?auto=format', 'none'),
('Chicken Soup', 15500, 'The only soup you`ll ever need', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ed137788-7c28-4e5c-805c-d852aae9bc3d_f9583311-e23b-4c5e-9bea-c74fcb1a14df.jpg?auto=format', 'soup'),
('Perkedel', 12000, 'Homemade potato cake, just like mom`s', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/bf85bb37-82da-4f21-8150-64b3fbd374d8_d911a3ad-981a-49d0-afac-572799f40683.jpg?auto=format', 'snack'),
('Scrambled Eggs', 17500, 'Fluffy eggs. Perfect with everything', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/642877e1-4771-4f09-b4a5-d4dcfdf74794_menu-item-image_1706770767357.jpg?auto=format', 'none'),
('Sunny Egg', 12000, 'Egg-cellent choice!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/5278111a-7357-428a-be13-124f890dedee_903ab711-484c-4b1e-bdfb-228ef6a377a9.jpg?auto=format', 'none'),
('Chicken Chunks', 28500, 'Tender-lovin Chicken Chunks 4pcs', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/0ba834d2-3650-4655-a1e7-3ac08998d0ae_menu-item-image_1686894048279.jpg?auto=format', 'snack'),
('Chicken Strips (3pcs)', 32500, '100% Premium Chicken Breast', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/d63740c6-f716-424a-9517-0b7df38dacf1_addaa6b9-703a-4429-b8cc-da87ffa40de3.jpg?auto=format', 'snack'),
('Curly Fries', 31000, 'A&W`s signature fries. Made of 100% USDA potatoes', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/701d6aff-b22b-495d-b68f-2e93f9ee179c_3d64bb13-e8c6-418d-8dd0-4d20afbc665c.jpg?auto=format', 'snack'),
('French Fries', 31000, 'Premium coated fries. Made of 100% USDA potatoes', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/7c4f1ea6-c2d0-4327-8e69-aa06fc320e7a_7b6bad88-75e8-4347-a447-f3752ba8f749.jpg?auto=format', 'snack'),
('Duo Fries', 59000, 'Double the fun! Made of 100% USDA potatoes', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ffc581be-92b7-4f8d-a2a3-e5fc5c8aeae5_45551217-b89c-45d8-9fd1-c1416aa77a60.jpg?auto=format', 'snack'),
('Chunks & Fries', 33500, 'Instant satisfaction!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/7c6c4908-b1e9-46f6-90ed-a95e2037e793_5134da30-0275-4cbb-afb8-6b87a4f6ef2d_file20190824-18867-1tjvkhr.jpeg?auto=format', 'snack'),
('Chunks & Curly Fries', 33500, 'Instant satisfaction!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/d3bca72e-e639-4f9a-9137-6959ab92528d_e27cbfa6-9ac1-4da7-b675-e01fca17d9cc_file20190904-10424-zi8k7a.jpeg?auto=format', 'snack'),
('Waffle & Chicken Strip', 17500, 'Great taste, great value', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/e48402dd-7150-41e5-8b52-5737bf0cb545_dc870f16-b0ca-4c86-b59f-74f1f4c665f7_file20190824-19636-1ruv2an.jpeg?auto=format', 'snack'),
('Mango Dip', 4500, 'Sweet and savory!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/db77cd79-a984-45c6-8803-ff9b0136ba8b_9a70464d-e95e-4ab7-9ffa-aed0d8f70de7_file20190824-19636-zteakc.jpeg?auto=format', 'none'),
('Cheese Dip', 4500, 'Cheese makes everything taste better!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/1796e646-1ac0-4300-930d-012b140d41a5_f07adbe4-891b-4bfc-a299-7ef88dbe5516_file20190824-18867-26n2l9.jpeg?auto=format', 'none'),
('Mayo Dip', 4500, 'Creamy classic goodness!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/74e6f996-0505-4634-9d37-71364fdc51ee_524d042e-53cd-41e5-a384-a7c0ac6d6446_master-menu-item-image_1583995238320.jpg?auto=format', 'none'),
('Durian Balls', 31500, '4pcs Durian Balls. MUST TRY!', 3, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/dafb356f-d69a-4466-84c9-abbae82ee303_menu-item-image_1668061516131.jpg?auto=format', 'snack'),

-- Desserts 
('Waffle Regular - Ice Cream', 44500, 'Crispy on the outside, fluffy on the inside. Served with a generous dose of vanilla softserve and chocolate/strawberry sauce', 4, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/28b20139-f646-42e2-897b-390b5be184e6_9855b3a7-8dda-48a2-b1f5-1e81cb65fa9e_file20190522-6246-6863ah.jpeg?auto=format', 'icecream'),
('Waffle Regular - Butter', 44500, 'Crispy on the outside, fluffy on the inside. Served with premium butter and waffle syrup', 4, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/dc3f1166-aba8-4c40-b943-c4ee278a03ea_88ce82d4-9da3-4f2f-a557-f333f2315bf6_file20190522-30091-lbzbb2.jpeg?auto=format', 'none'),
('Sundae', 16500, 'Choice of: Chocolate or Strawberry topping', 4, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/7fbee79b-e5c7-45e5-8d28-10620b1631d4_menu-item-image_1733554569452.jpg?auto=format', 'icecream'),
('Mountain Sundae', 24500, 'Bigger is always better', 4, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/81b02a33-bd12-437d-90dd-364119dfb8e5_menu-item-image_1733554564547.jpg?auto=format', 'icecream'),
('Durian Balls Mountain Sundae', 32500, 'Extra Large Sundae with 2 Durian Balls. (Chocolate/Strawberry/Vanilla)', 4, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/f9e942ff-7caa-4372-aa33-9c59b9a4a71b_menu-item-image_1677494198497.jpg?auto=format', 'icecream'),
('Milkshake', 26500, 'Choice of: Chocolate, Strawberry or Vanilla', 4, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/8fcf04e7-7b2c-4a0b-a72a-2cc355b1726c_b595d7ec-1cad-4226-9828-80c57b58fcb1_file20190522-6246-1oxe8b2.jpeg?auto=format', 'none'),

-- Paket
('Japanese Curry Chicken, Rice & RB', 74500, '2 Japanese Curry Chicken + 1 Rice + 1 Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/45f97785-a5dd-4a7a-9a9d-9edb82c29d5b_menu-item-image_1731255152565.jpg?auto=format', 'general'),
('Japanese Curry Chicken, Fries & RB', 89000, '2 Japanese Curry Chicken + 1 Fries (Curly/French) + 1 Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/4cac4820-2268-4520-8fef-d04872c7bc71_menu-item-image_1731255153433.jpg?auto=format', 'general'),
('Japanese Curry Double Beef Burger & RB', 57000, '1 Japanese Curry Double Beef Burger + 1 Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/534356b8-5131-41e9-8f9f-4fcb6bdb485c_menu-item-image_1731255154345.jpg?auto=format', 'general'),
('Double Cheese Burger, Fries & RB', 76000, '1 Double Cheese Burger + Fries (Curly/French) + Reg RB', 5 , 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/94c25fe4-468d-4138-8c8b-659c2b8a1283_menu-item-image_1730272858902.jpg?auto=format', 'general'),
('Double Cheese Burger & RB', 49500, '1 Double Cheese Burger + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/dde3c47a-6cde-4c33-b9f1-8dc72f17793f_menu-item-image_1730272860668.jpg?auto=format', 'general'),
('SUPER WednesDeals - 2 Mozza Burgers', 58000, '2 Mozza Burgers', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/b06f7df5-c460-4ec6-8a14-c4691f587d2c_menu-item-image_1732975397221.jpg?auto=format', 'general'),
('SUPER - 2 Aroma Chicken, Chic Sandwich & Rice', 88000, '2 Golden/Spicy Aroma Chicken + 1 Jumbo Chicken Sandwich + 1 Rice', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/066bff4c-fc9e-4009-a73d-06afd82aaedb_menu-item-image_1732973910278.jpg?auto=format', 'general'),
('SUPER - 2 Aroma Chicken, Cheese Burger & Fries', 95000, '2 Golden/Spicy Aroma Chicken + 1 Cheese Burger + 1 Fries (Curly/French)', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/e7c41a9f-a20f-4c75-a8ff-66664020d1b6_menu-item-image_1732973908600.jpg?auto=format', 'general'),
('SUPER - 2 Aroma Chicken, Deluxe Burger, Rice & RB', 117000, '2 Golden/Spicy Aroma Chicken + 1 Deluxe Burger + 2 Rice + 2 RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/cff0926c-f3cd-45d5-ab09-ea17a56abb62_menu-item-image_1732972996330.jpg?auto=format', 'general'),
('SUPER - 3 Aroma Chicken, Rice, Soup & Fries', 120000, '3 Golden/Spicy Aroma Chicken + 2 Rice + 1 Chicken Soup + 1 Fries (Curly/French)', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/0e94ac99-55bf-45a4-a794-9375c13a0498_menu-item-image_1732973907024.jpg?auto=format', 'general'),
('SUPER - 4 Aroma Chicken, Double Cheese Burger & Fries', 140000, '4 Golden/Spicy Aroma Chicken + 1 Double Cheese Burger + 1 Fries (Curly/French)', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/b1579490-fa91-4071-97f8-89d938e58fe9_menu-item-image_1732973905312.jpg?auto=format', 'general'),
('SUPER - 5 Aroma Chicken, Fries, Rice & RB', 178000, '5 Golden/Spicy Aroma Chicken + 1 Fries (Curly/French) + 3 Rice + 2 Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/935fc4fc-8109-4363-a6dc-65c30dae9af8_menu-item-image_1732973903555.jpg?auto=format', 'general'),
('SUPER - 6 Aroma Chicken, Fries, Rice & RB', 197000, '6 Golden/Spicy Aroma Chicken + 1 Fries (Curly/French) + 3 Rice + 2 Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/ee87499a-431b-4014-af42-1f2c41f552c9_menu-item-image_1732974371200.jpg?auto=format', 'general'),
('KETO Triple Beef Burger Combo', 97000, 'KETO Triple Beef Burger + 4pcs Chicken Chunks + 1 Aqua', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/51b1ae8d-8d3a-4156-a383-367273c92f1a_menu-item-image_1716975028975.jpg?auto=format', 'general'),
('KETO Double Spicy Chic Burger Combo', 101000, 'KETO Double Spicy Chicken Burger + 4pcs Chicken Chunks + 1 Aqua', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/7205220e-02b2-4fab-ab85-9115a42915da_menu-item-image_1716975032152.jpg?auto=format', 'general'),
('KETO 2 Aroma Chicken Combo', 80000, '2 Golden/Spicy Aroma Chicken + 4pcs Chicken Chunks + 1 Aqua', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/8888f63c-9d1a-48e9-b416-8215cce7c1ff_menu-item-image_1716975036266.jpg?auto=format', 'general'),
('Paket Gratis 1 - Chicken', 65500, '2 Golden/Spicy Aroma Chicken + Rice + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/eb340bde-f145-411f-bffa-b2dda317b105_4ff50907-81f8-4322-adbb-5b6857d2ab72.jpg?auto=format', 'general'),
('Paket Gratis 2 - Chicken & Soup', 53000, 'Golden/Spicy Aroma Chicken + Chicken Soup + Rice + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/50ba9a8d-1531-49bf-8c44-7933adcff824_2381867e-9e3b-43b7-a3df-0ec997fa5ca7.jpg?auto=format', 'general'),
('Paket Gratis 3 - Chicken Chunks & Chicken', 62000, '4pcs Chicken Chunks + Golden/Spicy Aroma Chicken + Rice + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/a13e2edc-a8e9-4b76-8aeb-3a4b47ffc0ab_ae1c6837-0d1d-4817-ad4c-45660c0be564.jpg?auto=format', 'general'),
('Paket Gratis 4 - Deluxe Burger & Chicken', 73500, 'Deluxe Burger + Golden/Spicy Aroma Chicken + Chicken Soup + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/9d7e8ace-4667-4f7a-bdde-899768cfaa06_menu-item-image_1706770718115.jpg?auto=format', 'general'),
('Paket Gratis 5 - Chicken Sandwich & Chicken', 78500, 'Chicken Sandwich + Golden/Spicy Aroma Chicken + Chicken Soup + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/7ccc73e1-748b-4d60-a163-18b9765cede1_menu-item-image_1706770720944.jpg?auto=format', 'general'),
('Paket Gratis 6 - Blackpepper Mixbowl & Chicken', 72000, 'Blackpepper Mixbowl + Golden/Spicy Aroma Chicken + Chicken Soup + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/58ef78fb-326f-4aa9-9eeb-ae1baae643f7_menu-item-image_1657600450098.jpg?auto=format', 'general'),
('Paket Gratis 7 - Chicken Chunks', 65500, '8pcs Chicken Chunks + Rice + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/90873021-4313-4b66-86c5-b753cdae6da7_menu-item-image_1683694603731.jpg?auto=format', 'general'),
('Paket Kentang A - Mozza Burger', 80000, 'Mozza Burger + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/576ce307-ab41-4dfe-b7ab-4e50eb5b18c7_menu-item-image_1706770702297.jpg?auto=format', 'general'),
('Paket Kentang B - Deluxe Burger', 73000, 'eluxe Burger + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/163a10d2-a3cf-4ca2-a410-42f16b92f5a0_menu-item-image_1706770705391.jpg?auto=format', 'general'),
('Paket Kentang C - Fish Sandwich', 75000, 'Fish Sandwich + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/6c86ec2d-6112-4d90-9f3d-d6e1379acdbb_menu-item-image_1706770708206.jpg?auto=format', 'general'),
('Paket Kentang D - Duo Aroma Chicken', 80000, '2 Golden/Spicy Aroma Chicken + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/e5b8c28c-44ae-4a5d-8e6f-0eec71d63aa4_762be042-e1f2-4ff9-80c3-462622497510.jpg?auto=format', 'general'),
('Paket Kentang E - Chicken Sandwich', 76000, 'Chicken Sandwich + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/b072b9a8-9683-4a25-b213-321685099ca8_menu-item-image_1706770711243.jpg?auto=format', 'general'),
('Paket Kentang F - Mango Chicken Pocket', 70000, 'Mango Chicken Pocket + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/e0546dd2-6ec5-4506-b4b9-bbc8aa6ccc41_menu-item-image_1706770714411.jpg?auto=format', 'general'),
('Paket Kentang G - Chicken Chunks & Fries', 84000, '8pcs Chicken Chunks + Fries (Curly/French) + Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/670e3589-e492-4cce-9f78-450876db3b5b_menu-item-image_1683694615644.jpg?auto=format', 'general'),
('HHU 1 - Aroma Chicken, Rice & Egg', 40000, 'Golden/Spicy Aroma Chicken + Rice + Scrambled Eggs', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/84580c1d-c2ab-48b0-94d8-2bb9612e86a9_menu-item-image_1624251044911.jpg?auto=format', 'general'),
('HHU 2 - Aroma Chicken, Rice, Egg & Waffle', 40000, 'Golden/Spicy Aroma Chicken + Rice + Scrambled Egg (Mini) + Waffle (Mini)', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/5ea5096f-b2e9-43f4-8032-36c5bd9d3a83_menu-item-image_1624251162566.jpg?auto=format', 'general'),
('HHU 3 - Aroma Chicken, Rice & Beef', 40000, 'Golden/Spicy Aroma Chicken + Rice + Beef', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/03ab8fc4-372e-458b-a608-b9bc7b0fadfc_menu-item-image_1624251280414.jpg?auto=format', 'general'),
('Picnic Barrel 1 - Chicken & Burger', 124000, '2 Golden/Spicy Aroma Chicken + Beef Burger + 4 Chicken Chunks™ + Rice + 2 Reg RB + Free Barrel', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/184ebd39-8f91-4ea7-9cda-002326ac272c_bd7d5e94-c7b0-4c02-a59c-0a906ed22bcc.jpg?auto=format', 'general'),
('Picnic Barrel 2 - Chicken & Chicken Chunks™', 151500, '4 Golden/Spicy Aroma Chicken + 4 Chicken Chunks + 2 Rice + 2 Reg RB + Free Barrel', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/4efd5cc0-b3b6-4d5f-b6fd-947c22d5e86d_91376efd-701b-4c63-93cf-6dae098de676.jpg?auto=format', 'general'),
('Picnic Barrel 3 - Duo Aroma Chicken', 134000, '6 Golden/Spicy Aroma Chicken + Free Barrel', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/46080611-73d4-4db9-b9f5-9eda8062e070_f448806f-6dbe-43d4-8b43-7850a153604f.jpg?auto=format', 'general'),
('Good Friends - Chicken & Soup', 235000, '6 Golden/Spicy Aroma Chicken + 2 Chicken Soup + 4 Rice + 4 Reg RB', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/865155ff-1901-44b0-a56f-a1872788036d_85e87a61-c70a-4565-953d-9a356756a03f.jpg?auto=format', 'general'),
('Good Family - Duo Aroma Chicken', 191000, '9 Golden/Spicy Aroma Chicken', 5, 'https://i.gojekapi.com/darkroom/gofood-indonesia/v2/images/uploads/57687554-4354-4ed9-940d-e8988c52213c_ef86b50b-d138-4071-8f12-1a5b2813d3e8.jpg?auto=format', 'general');

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
('Spicy Aroma', 0);

-- jadi iki liaten seng product iku di field paling akhir onok type e jadi misale burger, modifier e apa ae seng cocok cocokin ama yang modifiers e 
INSERT INTO product_type_modifiers (product_type, modifier_id) VALUES
('burger', 1),  -- Golden Aroma
('burger', 2);  -- Spicy Aroma

