Console.WriteLine("Hello ! Please, tell us your name");
string Name = Console.ReadLine();
Console.WriteLine("Please, tell us your surname");
string Surname = Console.ReadLine();
Console.WriteLine("Please, tell us your age");
int Age = int.Parse(Console.ReadLine());
Console.WriteLine("Please, type your city(Berlin/London/Washington) : ");
string City = Console.ReadLine();
string Country="";
if (City == "Berlin")
{
    Country = "Germany";
}
else if (City == "London")
{
    Country = "England";
}
else if (City == "Washington")
{
    Country = "USA";
}
else
{
    Console.WriteLine("Unknown country, try again ! ");
}
if (Country == "Germany" || Country == "England")
{
    if (Age > 18)
    {
        Console.WriteLine("You can buy alcohol");
    }
    else
    {
        Console.WriteLine("You can not buy alcohol");
    }
}
else if (Country == "USA")
{
    if (Age > 21)
    {
        Console.WriteLine("You can buy alcohol");
    }
    else
    {
        Console.WriteLine("You can't buy alcohol, sorry !");
    }
}
