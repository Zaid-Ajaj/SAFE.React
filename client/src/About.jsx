import React from "react";

export const About = ({ title }) => {
    return (
        <div style={{ padding: 10 }}>
            <h1>{title}</h1>
            <p>This component is written in JSX and can be easily imported from Feliz applications.</p>
            <p>It accepts parameters or props like <strong style={{ color: "red" }}>title</strong> that come from F#</p>
            <p>You can learn all about Feliz <a href="https://zaid-ajaj.github.io/Feliz">here</a></p>
        </div>
    );
}