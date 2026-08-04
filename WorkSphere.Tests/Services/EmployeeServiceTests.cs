using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.Exceptions;
using WorkSphere.Application.Interfaces;
using WorkSphere.Application.Services.Employees;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Tests.Services;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly Mock<IPositionRepository> _positionRepositoryMock;
    private readonly Mock<IBackgroundJobService> _backgroundJobServiceMock;


    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();

        _departmentRepositoryMock = new Mock<IDepartmentRepository>();

        _positionRepositoryMock = new Mock<IPositionRepository>();

        _backgroundJobServiceMock = new Mock<IBackgroundJobService>();

        _employeeService = new EmployeeService(
            _employeeRepositoryMock.Object,
            _departmentRepositoryMock.Object,
            _positionRepositoryMock.Object,
            _backgroundJobServiceMock.Object);
    }

    [Fact]
    public async Task DeleteAsync_EmployeeExists_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();

        var employee = new Employee
        {
            Id = id,
            FirstName = "David",
            LastName = "Palacio",
            Email = "david@test.com"
        };

        _employeeRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employee);

        // Act
        var result = await _employeeService.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();

        _employeeRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _employeeRepositoryMock.Verify(
            x => x.DeleteAsync(employee),
            Times.Once());
    }

    [Fact]
    public async Task DeleteAsync_EmployeeDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();


        _employeeRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Employee?)null);

        // Act
        var result = await _employeeService.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();

        _employeeRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _employeeRepositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<Employee>()),
            Times.Never());
    }

    [Fact]
    public async Task UpdateAsync_DepartmentDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        var request = new UpdateEmployeeRequest
        {
            DepartmentId = Guid.NewGuid(),
            PositionId = Guid.NewGuid(),
            FirstName = "David",
            LastName = "Palacio",
            Email = "david@test.com",
            Phone = "123456789",
            Salary = 3000
        };

        var employee = new Employee
        {
            Id = id
        };

        _employeeRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employee);

        _departmentRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Department?)null);

        // Act
        Func<Task> act = () => _employeeService.UpdateAsync(id, request);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>();

        _employeeRepositoryMock.Verify(
        x => x.GetByIdAsync(It.IsAny<Guid>()),
        Times.Once());

        _departmentRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _employeeRepositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Employee>()),
        Times.Never());

    }


    [Fact]
    public async Task UpdateAsync_EmployeeDoesNotExist_ReturnsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UpdateEmployeeRequest();

        _employeeRepositoryMock
           .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync((Employee?)null);

        // Act
        var result = await _employeeService.UpdateAsync(id, request);

        // Assert
        result.Should().BeNull();

        _employeeRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _employeeRepositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Employee>()),
            Times.Never());
    }



    [Fact]
    public async Task UpdateAsync_ValidRequest_UpdatesEmployeeSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();

        var request = new UpdateEmployeeRequest
        {
            DepartmentId = Guid.NewGuid(),
            PositionId = Guid.NewGuid(),
            FirstName = "David",
            LastName = "Palacio",
            Email = "david@test.com",
            Phone = "123456789",
            Salary = 3000
        };

        var employee = new Employee { Id = id };
        var department = new Department { Id = request.DepartmentId };
        var position = new Position { Id = request.PositionId };


        _employeeRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employee);

        _departmentRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(department);

        _positionRepositoryMock
           .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(position);

        // Act
        var result = await _employeeService.UpdateAsync(id, request);

        // Assert
        result.Should().NotBeNull();

        result.FirstName.Should().Be(request.FirstName);
        result.LastName.Should().Be(request.LastName);
        result.Email.Should().Be(request.Email);
        result.Phone.Should().Be(request.Phone);
        result.Salary.Should().Be(request.Salary);

        _employeeRepositoryMock.Verify(
        x => x.GetByIdAsync(It.IsAny<Guid>()),
        Times.Once());

        _departmentRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _positionRepositoryMock.Verify(
           x => x.GetByIdAsync(It.IsAny<Guid>()),
           Times.Once());

        _employeeRepositoryMock.Verify(
            x => x.UpdateAsync(It.Is<Employee>(e =>
            e.FirstName == request.FirstName &&
            e.LastName == request.LastName &&
            e.Email == request.Email)),
            Times.Once());
    }



    [Fact]
    public async Task UpdateAsync_PositionDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        var request = new UpdateEmployeeRequest
        {
            DepartmentId = Guid.NewGuid(),
            PositionId = Guid.NewGuid(),
            FirstName = "David",
            LastName = "Palacio",
            Email = "david@test.com",
            Phone = "123456789",
            Salary = 3000
        };

        var employee = new Employee { Id = id };
        var department = new Department { Id = request.DepartmentId };


        _employeeRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employee);

        _departmentRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(department);

        _positionRepositoryMock
          .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
          .ReturnsAsync((Position?)null);

        // Act
        Func<Task> act = () => _employeeService.UpdateAsync(id, request);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>();

        _employeeRepositoryMock.Verify(
        x => x.GetByIdAsync(It.IsAny<Guid>()),
        Times.Once());

        _departmentRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _positionRepositoryMock.Verify(
            x => x.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once());

        _employeeRepositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Employee>()),
        Times.Never());

    }


    [Fact]
    public async Task CreateAsync_ValidRequest_CreatesEmployeeSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();

        var request = new CreateEmployeeRequest
        {
            DepartmentId = Guid.NewGuid(),
            PositionId = Guid.NewGuid(),
            FirstName = "David",
            LastName = "Palacio",
            Email = "david@test.com",
            Phone = "123456789",
            Salary = 3000
        };

        var department = new Department { Id = request.DepartmentId };
        var position = new Position { Id = request.PositionId };

        _departmentRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(department);

        _positionRepositoryMock
           .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(position);
        // Act
        var result = await _employeeService.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();

        result.FirstName.Should().Be(request.FirstName);
        result.LastName.Should().Be(request.LastName);
        result.Email.Should().Be(request.Email);
        result.Phone.Should().Be(request.Phone);
        result.Salary.Should().Be(request.Salary);

        _positionRepositoryMock.Verify(
          x => x.GetByIdAsync(It.IsAny<Guid>()),
          Times.Once());

        _departmentRepositoryMock.Verify(
         x => x.GetByIdAsync(It.IsAny<Guid>()),
         Times.Once());

        _employeeRepositoryMock.Verify(
         x => x.AddAsync(It.Is<Employee>(e =>
             e.FirstName == request.FirstName &&
             e.LastName == request.LastName &&
             e.Email == request.Email)),
         Times.Once());

        _backgroundJobServiceMock.Verify(
         x => x.EnqueueEmail<INotificationService>(
        It.IsAny<Expression<Func<INotificationService, Task>>>()),
        Times.Once());

    }

}