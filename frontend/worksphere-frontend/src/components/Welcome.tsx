type WelcomeProps = {
    nombre: string;
};

function Welcome({ nombre }: WelcomeProps) {
    return <h2>Bienvenido {nombre}</h2>;
}

export default Welcome;