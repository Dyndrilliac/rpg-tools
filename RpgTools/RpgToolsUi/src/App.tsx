import { ChangeEvent, useState } from 'react';
import { LoginResponse } from './Models/Login/LoginResponse';
import DiceRoller from './Components/DiceRoller';
import './App.css';

function App() {
    const [loginForm, setLoginForm] = useState({
        email: '',
        password: '',
    })

    const changeHandler = (event: ChangeEvent<HTMLInputElement>) => {
        setLoginForm({ ...loginForm, [event.currentTarget.name]: event.currentTarget.value })
    }

    const [authenticated, setAuthenticated] = useState<LoginResponse>();

    async function populateLoginData() {
        const response = await fetch('https://localhost:32770/login', {
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
              },
            method: 'POST',
            body: JSON.stringify(loginForm),
        });
        
        const data = await response.json();
        setAuthenticated(data);
    }

    const contents = authenticated === undefined
        ? <div id="inputForm">
            <p>Please login.</p>
            <label>Email:</label><input type="email" id="formEmail" name="email" onChange={changeHandler} placeholder="Enter your email address."></input>
            <br />
            <label>Password:</label><input type="password" id="formPassword" name="password" onChange={changeHandler} placeholder="Enter your password."></input>
            <br />
            <button id="formLogin" onClick={populateLoginData}>Login</button>
          </div>
        : <DiceRoller {...authenticated} />

    return (
        <div id="page">
            <h1 id="title">Rpg Tools</h1>
            <div id="contents">
                {contents}
            </div>
        </div>
    );
}

export default App;