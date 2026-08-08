import { useContext, useState } from "react";
import { AuthContext } from "../context/AuthContext";


function LoginForm() {
  const { login } = useContext(AuthContext);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  

  const manejarSubmit = async (
    event: React.FormEvent<HTMLFormElement>
    ) => {
    event.preventDefault();

    setError("");

    if (!email.trim()) {
        setError("El email es obligatorio");
        return;
    }

    if (!password.trim()) {
        setError("La contraseña es obligatoria");
        return;
    }

    try {
        await login(email, password);

        console.log("Login exitoso");
    } catch (error) {
        setError("Email o contraseña incorrectos");
    }
};

  return (
    <form onSubmit={manejarSubmit}>
      <h2>Iniciar sesión</h2>

      <div>
        <label htmlFor="email">
          Email
        </label>

        <br />

        <input
          id="email"
          type="email"
          value={email}
          placeholder="admin@test.com"
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>

      <br />

      <div>
        <label htmlFor="password">
          Contraseña
        </label>

        <br />

        <input
          id="password"
          type="password"
          value={password}
          placeholder="123456"
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>

      <br />

      {error && <p>{error}</p>}

      <button type="submit">
        Iniciar sesión
      </button>
    </form>
  );
}

export default LoginForm;