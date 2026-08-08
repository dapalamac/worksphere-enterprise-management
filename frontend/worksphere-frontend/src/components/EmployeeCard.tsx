import type { Employee } from "../types/Employee";

interface EmployeeCardProps {
  employee: Employee;
}

function EmployeeCard({ employee }: EmployeeCardProps) {
  return (
    <div>
      <h3>{employee.nombre}</h3>
      <p>{employee.cargo}</p>
    </div>
  );
}

export default EmployeeCard;