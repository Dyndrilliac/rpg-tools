import { ChangeEvent, useState } from 'react';
import { LoginResponse } from '../Models/Login/LoginResponse';
import { RollResult } from '../Models/Dice/RollResult';

function DiceRoller(props: LoginResponse) {
    const [diceForm, setDiceForm] = useState({
        dice: 0,
        sides: 0,
    })

    const changeHandler = (event: ChangeEvent<HTMLInputElement>) => {
        setDiceForm({ ...diceForm, [event.currentTarget.name]: event.currentTarget.value })
    }

    const [roll, setRoll] = useState<RollResult>();

    function renderRoll() {
        if (roll !== undefined) {
            return (
                <tr key={roll.timestamp}>
                    <td>{roll.timestamp}</td>
                    <td>{roll.playerName}</td>
                    <td>{roll.dice}</td>
                    <td>{roll.sides}</td>
                    <td>{roll.total}</td>
                    <td>{roll.results.toString()}</td>
                </tr>
            );
        }
    }

    async function populateRollData() {
        const response = await fetch(`https://localhost:32770/roll?Dice=${diceForm.dice}&Sides=${diceForm.sides}`, {
            headers: { Authorization: `Bearer ${props.accessToken}` }
        });
        const data = await response.json();
        setRoll(data);
    }

    return (
        <div id="componentBody">
            <h2 id="componentLabel">Dice Roller</h2>
            <div id="inputForm">
                <label>Dice:</label><input type="number" id="formDice" name="dice" onChange={changeHandler} placeholder="Enter number of dice."></input>
                <br />
                <label>Sides:</label><input type="number" id="formSides" name="sides" onChange={changeHandler} placeholder="Enter number of sides on each die."></input>
                <br />
                <button id="formRoll" onClick={populateRollData}>Roll</button>
            </div>
            <div id="results">
                <h3 id="tabelLabel">Results</h3>
                <table className="table table-striped" aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Timestamp</th>
                            <th>Player Name</th>
                            <th>Dice</th>
                            <th>Sides</th>
                            <th>Total</th>
                            <th>Results</th>
                        </tr>
                    </thead>
                    <tbody>
                        {renderRoll()}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default DiceRoller;