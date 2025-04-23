using System;

public class Customer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }

    public Customer(string id, string name, string contact)
    {
        Id = id;
        Name = name;
        Contact = contact;
    }

    public override string ToString()
    {
        return $"客户ID: {Id}, 姓名: {Name}, 联系方式: {Contact}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Customer other = (Customer)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
} 