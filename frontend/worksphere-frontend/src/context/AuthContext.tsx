import { createContext, useState, type ReactNode } from "react";
import {
  getToken,
  login as loginService,
  logout as logoutService,
} from "../services/authService";

interface AuthContextType {
  isAuthenticated: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
}

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthContext = createContext<AuthContextType>({
  isAuthenticated: false,
  login: async () => {},
  logout: () => {},
});

export function AuthProvider({ children }: AuthProviderProps) {
  const [isAuthenticated, setIsAuthenticated] = useState(
    getToken() !== null
  );

  const login = async (email: string, password: string) => {
    const response = await loginService({
      email,
      password,
    });

    localStorage.setItem("token", response.token);

    setIsAuthenticated(true);
  };

  const logout = () => {
    logoutService();

    setIsAuthenticated(false);
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}