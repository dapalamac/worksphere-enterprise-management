using Moq;
using WorkSphere.Application.Interfaces;
using WorkSphere.Application.Services.Employees;

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


}