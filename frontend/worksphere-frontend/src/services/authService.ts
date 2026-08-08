import type { LoginRequest, LoginResponse } from "../types/Auth";

const API_URL = "https://localhost:7146/api";

export async function login(
  credentials: LoginRequest
): Promise<LoginResponse> {
  const response = await fetch(`${API_URL}/Auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  });

  if (!response.ok) {
    throw new Error("Credenciales incorrectas");
  }

  return await response.json();
}

export function saveToken(token: string): void {
  localStorage.setItem("token", token);
}

export function getToken(): string | null {
  return localStorage.getItem("token");
}

export function logout(): void {
  localStorage.removeItem("token");
}