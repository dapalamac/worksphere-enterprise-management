import { useContext } from "react";
import "./App.css";
//import EmployeeList from "./components/EmployeeList";
import LoginForm from "./components/LoginForm";
import { AuthContext } from "./context/AuthContext";

function App() {
  const { isAuthenticated, logout } = useContext(AuthContext);

  return (
    <div>
      <h1>WorkSphere</h1>

      {!isAuthenticated ? (
        <LoginForm />
      ) : (
        <div>
          <h2>Usuario autenticado</h2>

          <p>Has iniciado sesión correctamente.</p>

          <button onClick={logout}>
            Cerrar sesión
          </button>
        </div>
      )}
    </div>
  );
}


/*
function App() {
  const [nombre, setNombre] = useState("");
  const [email, setEmail] = useState("");
  

  const manejarSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    console.log("Nombre:", nombre);
    console.log("Email:", email);
  };

  return (
    <div>
      <h1>WorkSphere</h1>

      <h2>Formulario de usuario</h2>

      <form onSubmit={manejarSubmit}>

        <input
          type="text"
          placeholder="Nombre"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
        />

        <br />
        <br />

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <br />
        <br />

        <button type="submit">
          Guardar
        </button>

      </form>
    </div>
  );
}


function App() {

   const [contador, setContador] = useState(0);

  return (
    <div>
      <h1>Contador: {contador}</h1>

      <button onClick={() => setContador(contador + 1)}>
        Incrementar
      </button>
    </div>
  );


  return (
    <div>
      
      <h1>🚀 WorkSphere</h1>

      <EmployeeCard
        firstName="David"
        lastName="García"
        position="Desarrollador Frontend"
        salary={50000}
      />
      <Welcome nombre="David" />

      <Texto />

      <Botón />
    </div>
  );
  */


export default App;
