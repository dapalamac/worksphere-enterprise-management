import type { Employee } from "../types/Employee";

const API_URL = "https://localhost:7146/api";

export async function getEmployees(): Promise<Employee[]> {
  const response = await fetch(`${API_URL}/Employees`);

  if (!response.ok) {
    throw new Error("Error al obtener los empleados");
  }

  return await response.json();
}