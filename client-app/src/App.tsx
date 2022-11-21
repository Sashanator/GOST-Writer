import { useState } from "react";
import "./App.scss";
import "antd/dist/reset.css";
import { formatWordDocument } from "./data-contexts/wordDataContext";
import { Header } from "./components/Header";
import { Homepage } from "./pages/homepage/Homepage";

function App() {
    

    return (
        <>
            {/* <Header /> */}
            <Homepage />
        </>
        // <div className="App">
        //     <form onSubmit={handleSubmit}>
        //         <h1>React File Upload</h1>
        //         <input type="file" onChange={handleChange} />
        //         <button type="submit">Upload</button>
        //     </form>
        // </div>
    );
}

export default App;
