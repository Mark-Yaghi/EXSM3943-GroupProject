EXSM 3943: C# II - Project thepeasants, https://ualberta-fullstack.slack.com/archives/C03TSQGTWTW https://meet.google.com/hrp-iorw-rxs

******Passwrod to enter the admin section is : "password"*********************************

Introduction: This is a small group project to build a grocery story database using C# and Microsoft Entity Framework Core. Team members include: Mark Yaghi, Arv Pandit, Nick Mbugua, Kristian Denis.

We allocated one or more sections of the project (the models Customer.cs, Order.cs, OrderDetail.cs, Products.cs, Supplier.cs, and the context DatabaseContext.cs) to different group members. Each group member worked independently, with daily meetings as needed to ensure the pieces fit together (GitHub branches merged).

Five entity roles:

“Customer” model: tracks customer/client information, where a client may be a person or a company. The information stored in the model is: customer ID, first name, last name, and address.

“Product” model: tracks inventory for a grocery store. The information stored in this model is: product ID, product name, product description, quantity in stock, and sale price.

“Order” model: records details of individual orders from the store. Also tracks an association between an order placed and the customer who placed it. Also contributes to an association between the order placed and details of the products in it. The information stored in this model is: order ID, customer ID, total invoice amount, and order date.

"OrderDetail" model: records details of individual products in all orders placed. Works to relate the Order model to the inventory tracked in the Product model. The information stored in this model is: order detail ID, order ID, product ID, and quantity ordered.

"Supplier" model: records information related to the suppliers who provide inventory to the store. The information stored in this model is: company ID, company name, company address, and contact phone number.

Database context: DatabaseContext.cs creates the five models, establishes primary and foreign keys for them, and seeds them with data.

Program.cs: implementation of an admin password using a hardcoded value. "password" This is not secure creation of a main user-facing menu with an exit option. The program repeats (show the menu when an option is done), until the exit option is chosen, option to make a purchase. The user is be prompted for an identifier (first/last name, customer number, company name, etc). If they do not exist, take the necessary information to create the customer.The user is shown a menu of products to choose from, they pick one and enter a quantity. An order will be generated and inventory will be deducted if there is sufficient inventory, and the ability to put multiple items in a menu.

Admin logout: (logs out of admin mode).

Add product: which allows a new product to be added. Add inventory: which allows inventory to be added to an existing product). Discontinue item: which marks an existing item as discontinued and disallows further inventory additions (can still be sold).

Division of Labour:

Mark Yaghi: Wrote DatabaseContext.cs; converted password input to asterisks, wrote the "Add Inventory" section in the Program.cs.

Arv Pandit: Created the entire skeleton of the program.cs including user-facing menus, shopping cart, receipt, adding the user in database if doesn't exisits.  

Nick Mbugua: I wrote the Models for, Customer, Order, Order Detail and Product. Wrote the add a new product and the discontinued a product in the program.cs

Kristian Denis: Wrote the model Supplier.cs, impimented and seeded this model in DatabaseContext.cs, and wrote README.md.