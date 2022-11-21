import { ConfigProvider } from "antd";
import React, { useState } from "react";
import "./App.scss";
import { Homepage } from "./pages/homepage/Homepage";
import "antd/dist/reset.css";
import axios, { AxiosRequestConfig } from "axios";
import { WORD_SERVICE } from "./data-contexts/configuration";
import { formatWordDocument } from "./data-contexts/wordDataContext";

function App() {
    const [file, setFile] = useState<any>();

    function handleChange(event: any) {
        setFile(event.target.files[0]);
    }
    async function handleSubmit(event: any) {
        event.preventDefault();
        const data = await formatWordDocument(file);
        const outputFileName = `RESULT.docx`;
        const url = URL.createObjectURL(new Blob([data]));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', outputFileName);
        document.body.appendChild(link);
        link.click();
    }

    return (
        <div className="App">
            <form onSubmit={handleSubmit}>
                <h1>React File Upload</h1>
                <input type="file" onChange={handleChange} />
                <button type="submit">Upload</button>
            </form>
        </div>
    );
}

export default App;
