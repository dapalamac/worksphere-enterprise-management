import { useEffect, useState } from "react";
import EmployeeCard from "./EmployeeCard";
import { getEmployees } from "../services/employeeService";
import type { Employee } from "../types/Employee";

function EmployeeList() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const cargarEmpleados = async () => {
      try {
        const data = await getEmployees();

        setEmployees(data);
      } catch (error) {
        setError("No se pudieron cargar los empleados");
      } finally {
        setLoading(false);
      }
    };

    cargarEmpleados();
  }, []);

  if (loading) {
    return <p>Cargando empleados...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  return (
    <div>
      {employees.map((employee) => (
        <EmployeeCard
          key={employee.id}
          employee={employee}
        />
      ))}
    </div>
  );
}

export default EmployeeList;