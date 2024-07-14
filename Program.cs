using System;
using System.Collections.Generic;
using System.Linq;

namespace Azenride
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<Order> orderHistory = new List<Order>();
        const string promoCode = "AZENRIDEXYZ"; // Example promo code
        const double promoDiscount = 0.2; // 20% discount for promo code

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Mabuhay! Welcome to AzenRide!");
                Console.WriteLine("Please select an option (1 to 3):");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                int choice = GetValidChoice(1, 3);

                if (choice == 1)
                {
                    Registration();
                }
                else if (choice == 2)
                {
                    Login();
                }
                else if (choice == 3)
                {
                    break;
                }
            }
        }

        static void Registration()
        {
            Console.WriteLine("Kindly Register your account");

            string username;
            do
            {
                Console.Write("Enter your Username: ");
                username = Console.ReadLine();
            } while (string.IsNullOrEmpty(username));

            string password;
            do
            {
                Console.Write("Enter your Password: ");
                password = Console.ReadLine();
            } while (string.IsNullOrEmpty(password));

            string contactNumber;
            do
            {
                Console.Write("Enter your Contact Number: ");
                contactNumber = Console.ReadLine();
            } while (string.IsNullOrEmpty(contactNumber));

            string address;
            do
            {
                Console.Write("Enter your Address: ");
                address = Console.ReadLine();
            } while (string.IsNullOrEmpty(address));

            users.Add(new User(username, password, contactNumber, address));
            Console.WriteLine("Registration successful! Mabuhay Welcome to AzenRide!");
        }

        static void Login()
        {
            Console.Write("Enter your Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your Password: ");
            string password = Console.ReadLine();

            User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"Welcome, {username}!");
                UserMenu(user);
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        static void UserMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Use Azenride");
                Console.WriteLine("2. View Order History");
                Console.WriteLine("3. Update Profile");
                Console.WriteLine("4. Manage Favorite Locations");
                Console.WriteLine("5. Refer a Friend");
                Console.WriteLine("6. View Notifications");
                Console.WriteLine("7. Exit");
                Console.WriteLine("Please select an option (1 to 7):");
                int choice = GetValidChoice(1, 7);

                if (choice == 1)
                {
                    UseAzenRide(user);
                }
                else if (choice == 2)
                {
                    ViewOrderHistory(user);
                }
                else if (choice == 3)
                {
                    UpdateProfile(user);
                }
                else if (choice == 4)
                {
                    ManageFavoriteLocations(user);
                }
                else if (choice == 5)
                {
                    InviteFriend(user);
                }
                else if (choice == 6)
                {
                    ViewNotifications(user);
                }
                else if (choice == 7)
                {
                    break;
                }
            }
        }

        static void ViewOrderHistory(User user)
        {
            Console.WriteLine("Order History:");
            var userOrders = orderHistory.Where(order => order.Username == user.Username).ToList();
            if (userOrders.Count == 0)
            {
                Console.WriteLine("No orders found.");
            }
            else
            {
                foreach (var order in userOrders)
                {
                    Console.WriteLine($"Service: {order.Service}, Cost: {order.Cost} pesos, " +
                                      $"Pickup:  {order.PickupLocation}, Drop-off: {order.DropoffLocation}");
                }
            }
        }

        static void UpdateProfile(User user)
        {
            Console.WriteLine("Update Profile:");

            Console.Write("Current Contact Number: ");
            string newContactNumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(newContactNumber) && long.TryParse(newContactNumber, out _))
            {
                user.ContactNumber = newContactNumber;
                Console.WriteLine("Contact number updated successfully.");
            }
            else if (!string.IsNullOrEmpty(newContactNumber))
            {
                Console.WriteLine("Invalid contact number. No changes made.");
            }

            Console.Write("Current Address: ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAddress))
            {
                user.Address = newAddress;
                Console.WriteLine("Address updated successfully.");
            }
            else
            {
                Console.WriteLine("No changes made to the address.");
            }
        }

        static void ManageFavoriteLocations(User user)
        {
            Console.WriteLine("Manage Favorite Locations:");
            Console.WriteLine("1. Add Favorite Location");
            Console.WriteLine("2. View Favorite Locations");
            Console.WriteLine("3. Remove Favorite Location");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Please select an option (1 to 4):");
            int choice = GetValidChoice(1, 4);

            if (choice == 1)
            {
                Console.Write("Enter a new favorite location: ");
                string location = Console.ReadLine();
                if (!user.FavoriteLocations.Contains(location))
                {
                    user.FavoriteLocations.Add(location);
                    Console.WriteLine("Location added to favorites.");
                }
                else
                {
                    Console.WriteLine("Location already exists in favorites.");
                }
            }
            else if (choice == 2)
            {
                Console.WriteLine("Favorite Locations:");
                foreach (var location in user.FavoriteLocations)
                {
                    Console.WriteLine(location);
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Enter the location to remove from favorites:");
                string location = Console.ReadLine();
                if (user.FavoriteLocations.Remove(location))
                {
                    Console.WriteLine("Location removed from favorites.");
                }
                else
                {
                    Console.WriteLine("Location not found in favorites.");
                }
            }
        }

        static void InviteFriend(User user)
        {
            Console.WriteLine("Invite a Friend: ");
            Console.Write("Enter your friend's email: ");
            string friendEmail = Console.ReadLine();
            if (string.IsNullOrEmpty(friendEmail) || !friendEmail.Contains('@'))
            {
                Console.WriteLine("Invalid email address.");
            }
            else
            {
                Console.WriteLine("Referral sent to your friend's email.");
                Console.WriteLine("Thank you for referring your friend. Please share it next time. Thank you!");
            }
        }

        static void ViewNotifications(User user)
        {
            if (user.Notifications.Count == 0)
            {
                Console.WriteLine("You have no notifications.");
            }
            else
            {
                Console.WriteLine("Notifications:");
                foreach (var notification in user.Notifications)
                {
                    Console.WriteLine(notification);
                }
            }
        }

        static void UseAzenRide(User user)
        {
            double totalCost = 0.0;

            Console.WriteLine("\nWhich service would you like to use?");
            Console.WriteLine("Please select a service (1 to 3):");
            Console.WriteLine("1. Delivery Pick up");
            Console.WriteLine("2. Driver Pick up");
            Console.WriteLine("3. Schedule a Ride");
            int choice = GetValidChoice(1, 3);

            Console.Write("Enter promo code (if any, otherwise press Enter): ");
            string enteredPromoCode = Console.ReadLine();

            if (!string.IsNullOrEmpty(enteredPromoCode) && enteredPromoCode.ToUpper() == promoCode)
            {
                Console.WriteLine("Promo code applied successfully!");
                // Apply a 20% discount for the promo code
                totalCost -= totalCost * promoDiscount;
            }

            if (choice == 1)
            {
                totalCost += HandleDeliveryPickUp(user);
            }
            else if (choice == 2)
            {
                totalCost += HandleDriverPickUp(user);
            }
            else if (choice == 3)
            {
                totalCost += ScheduleRide(user);
            }

            Console.WriteLine($"Total Cost: {totalCost} pesos");

            orderHistory.Add(new Order(user.Username, "Service", totalCost, "", ""));
            Console.WriteLine("Order added to order history.");

            user.Notifications.Add($"You have successfully placed an order with total cost of {totalCost} pesos.");
        }

        static double HandleDeliveryPickUp(User user)
        {
            List<double> itemWeights = new List<double>();

            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Van");
            Console.WriteLine("4. CarfourSeater");
            Console.WriteLine("5. Sedan");

            Console.Write("Enter your vehicle choice (1 to 5): ");
            int vehicleChoice = GetValidChoice(1, 5);

            string vehicleType = vehicleChoice switch
            {
                1 => "Car",
                2 => "Motorcycle",
                3 => "Van",
                4 => "CarfourSeater",
                5 => "Sedan",
                _ => throw new ArgumentOutOfRangeException()
            };

            string itemType;
            do
            {
                Console.Write("Enter the type of item: ");
                itemType = Console.ReadLine();
            } while (string.IsNullOrEmpty(itemType));

            double itemWeight;
            do
            {
                Console.Write("Enter the weight of the item (kg): ");
            } while (!double.TryParse(Console.ReadLine(), out itemWeight) || itemWeight <= 0);

            itemWeights.Add(itemWeight);

            string pickUpLocation;
            do
            {
                Console.Write("Enter the pick-up location: ");
                pickUpLocation = Console.ReadLine();
            } while (string.IsNullOrEmpty(pickUpLocation));

            string dropOffLocation;
            do
            {
                Console.Write("Enter the drop-off location: ");
                dropOffLocation = Console.ReadLine();
            } while (string.IsNullOrEmpty(dropOffLocation));

            double totalCost = 50.0 + itemWeights.Sum(weight => 5.0 * weight);
            Console.WriteLine($"Total Cost: {totalCost} pesos");

            orderHistory.Add(new Order(user.Username, "Delivery Pick up", totalCost, pickUpLocation, dropOffLocation));
            Console.WriteLine("Order added to order history.");

            user.Notifications.Add($"You have successfully placed an order for {itemType} using {vehicleType} with total cost of {totalCost} pesos.");

            return totalCost;
        }

        static double HandleDriverPickUp(User user)
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Van");
            Console.WriteLine("4. CarfourSeater");
            Console.WriteLine("5. Sedan");

            Console.Write("Enter your vehicle choice (1 to 5): ");
            int vehicleChoice = GetValidChoice(1, 5);

            string vehicleType = vehicleChoice switch
            {
                1 => "Car",
                2 => "Motorcycle",
                3 => "Van",
                4 => "CarfourSeater",
                5 => "Sedan",
                _ => throw new ArgumentOutOfRangeException()
            };

            string pickUpLocation;
            do
            {
                Console.Write("Enter the pick-up location: ");
                pickUpLocation = Console.ReadLine();
            } while (string.IsNullOrEmpty(pickUpLocation));

            string dropOffLocation;
            do
            {
                Console.Write("Enter the drop-off location: ");
                dropOffLocation = Console.ReadLine();
            } while (string.IsNullOrEmpty(dropOffLocation));

            double totalCost = 100.0;
            Console.WriteLine($"Total Cost: {totalCost} pesos");

            orderHistory.Add(new Order(user.Username, "Driver Pick up", totalCost, pickUpLocation, dropOffLocation));
            Console.WriteLine("Order added to order history.");

            user.Notifications.Add($"You have successfully placed a driver pick up order using {vehicleType} with total cost of {totalCost} pesos.");

            return totalCost;
        }

        static double ScheduleRide(User user)
        {
            Console.WriteLine("Enter the number of rides you would like to schedule: ");
            int numRides = int.Parse(Console.ReadLine());
            double totalCost = 0.0;
            for (int i = 0; i < numRides; i++)
            {
                Console.Write("Enter the pick-up location: ");
                string pickUpLocation = Console.ReadLine();

                Console.Write("Enter the drop-off location: ");
                string dropOffLocation = Console.ReadLine();

                double rideCost = 70.0;
                totalCost += rideCost;
                Console.WriteLine($"Total Cost: {rideCost} pesos");

                orderHistory.Add(new Order(user.Username, "Scheduled Ride", rideCost, pickUpLocation, dropOffLocation));
                Console.WriteLine("Order added to order history.");

                user.Notifications.Add($"You have successfully scheduled a ride with total cost of {rideCost} pesos.");
            }

            return totalCost;
        }

        static int GetValidChoice(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine($"Please enter a valid choice ({min}-{max}).");
            }
            return choice;
        }

        static double GetPositiveDouble()
        {
            double value;
            while (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.WriteLine("Please enter a positive number.");
            }
            return value;
        }
    }
}