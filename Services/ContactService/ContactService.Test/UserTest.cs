using ContactService.Application.Services;
using ContactService.Domain.Dto;
using ContactService.Domain.Entities;
using ContactService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using ContactService.Infrastructure.Contexts;
using ContactService.Infrastructure.Repositories;

namespace ContactService.Test;

public class Tests
{
    private DbContextOptions<ContactDbContext> dbContextOptions = new DbContextOptionsBuilder<ContactDbContext>()
        .UseInMemoryDatabase(databaseName: "ContactDb")
        .Options;
    
    private UserService userService;
    private Guid userGuid;
    
    [SetUp]
    public void Setup()
    {
        SeedDb();
        var context = new ContactDbContext(dbContextOptions);
        var userRepository = new UserRepository(context);
        var userContactInfoRepository = new UserContactInfoRepository(context);
        userService = new UserService(userRepository, userContactInfoRepository);
    }
    private void SeedDb()
    {
        using var context = new ContactDbContext(dbContextOptions);
        var users = new List<User>
        {
            new User { Name = "Burak Koray", Surname = "Balcı", CompanyName = "Balcı"},
            new User { Name = "John", Surname = "Doe", CompanyName = "ACME"},
            new User { Name = "Jane", Surname = "Doe", CompanyName = "ACME"},
        };

        var contactInfos = new List<UserContactInfo>
        {
            new UserContactInfo{ ContactType = ContactType.Phone, ContactTypeValue = "5448645353", User = users[0] },
            new UserContactInfo{ ContactType = ContactType.Phone, ContactTypeValue = "5448644458", User = users[1] },
            new UserContactInfo{ ContactType = ContactType.Location, ContactTypeValue = "Istanbul", User = users[2] },
        };

        context.AddRange(users);
        context.AddRange(contactInfos);
        context.SaveChanges();
    }

    [Test]
    public async Task Create_User_Successful()
    {
        var response = await userService.CreateUser(new CreateUserDto()
        {
            Name = "Test",
            Surname = "1",
            CompanyName = "ACME",
        });
        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.StatusCode, Is.EqualTo(201));
    }
    
    [Test]
    public async Task Remove_User_Successful()
    {
        var response = await userService.GetUsers();
        var user = response.Data.First();
        var removeUserResponse = await userService.RemoveUser(user.UUID);
        Assert.That(removeUserResponse.IsSuccessful, Is.True);
        Assert.That(removeUserResponse.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task Create_User_Fail()
    {
        var response = await userService.CreateUser(new CreateUserDto()
        {
            Name = "John",
            Surname = "Doe",
            CompanyName = "ACME",
        });
        Assert.That(response.IsSuccessful, Is.False);
        Assert.That(response.StatusCode, Is.EqualTo(400));
    }
    
    [Test]
    public async Task Get_Users_Successful()
    {
        var response = await userService.GetUsers();
        Assert.That(response.IsSuccessful, Is.True);
        Assert.That(response.Data.Count > 0, Is.True);
    }
    
    [Test]
    public async Task Get_UserById_Successful()
    {
        var response = await userService.GetUsers();
        var user = response.Data.First();
        var getByUserIdResponse = await userService.GetUserById(user.UUID);
        Assert.That(getByUserIdResponse.IsSuccessful, Is.True);
        Assert.That(getByUserIdResponse.Data, Is.Not.Null);
        Assert.That(getByUserIdResponse.Data.ContactInfos.Count > 0, Is.True);
    }
    
    [Test]
    public async Task Add_UserContactInfo_Successful()
    {
        var response = await userService.GetUsers();
        var users = response.Data;
        var user = users.Find(x => x.Name == "John" && x.Surname == "Doe");
        var addContactInfoResponse = await userService.AddContactInfo(new AddContactInfoDto
        {
            UserId = user.UUID,
            ContactType = ContactType.Phone,
            ContactTypeValue = "5444445522"
        });
        Assert.That(addContactInfoResponse.IsSuccessful, Is.True);
        Assert.That(addContactInfoResponse.StatusCode, Is.EqualTo(201));
    }
    
    [Test]
    public async Task Add_UserContactInfo_Fail()
    {
        var response = await userService.GetUsers();
        var users = response.Data;
        var user = users.Find(x => x.Name == "John" && x.Surname == "Doe");
        var addContactInfoResponse = await userService.AddContactInfo(new AddContactInfoDto
        {
            UserId = user.UUID,
            ContactType = ContactType.Phone,
            ContactTypeValue = "5448644458"
        });
        Assert.That(addContactInfoResponse.IsSuccessful, Is.False);
        Assert.That(addContactInfoResponse.StatusCode, Is.EqualTo(400));
    }
}