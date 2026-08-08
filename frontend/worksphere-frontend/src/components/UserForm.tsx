import { useState } from "react";

function UserForm() {
  const [nombre, setNombre] = useState("");
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");

  const manejarSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    setError("");

    if (!nombre.trim()) {
      setError("El nombre es obligatorio");
      return;
    }

    if (!email.trim()) {
      setError("El email es obligatorio");
      return;
    }

    if (!email.includes("@")) {
      setError("El email no tiene un formato válido");
      return;
    }

    console.log("Formulario enviado");
    console.log("Nombre:", nombre);
    console.log("Email:", email);
  };

  return (
    <form onSubmit={manejarSubmit}>
      <div>
        <label htmlFor="nombre">
          Nombre
        </label>

        <br />

        <input
          id="nombre"
          type="text"
          placeholder="Escribe tu nombre"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
        />
      </div>

      <br />

      <div>
        <label htmlFor="email">
          Email
        </label>

        <br />

        <input
          id="email"
          type="email"
          placeholder="ejemplo@email.com"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>

      <br />

      {error && <p>{error}</p>}

      <button type="submit">
        Guardar
      </button>
    </form>
  );
}

export default UserForm;