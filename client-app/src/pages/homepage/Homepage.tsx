import { Button } from "antd";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useState } from "react";
import {
    formatWordDocument,
} from "../../data-contexts/wordDataContext";
import "./Homepage.scss";
import { faPaperPlane } from "@fortawesome/free-regular-svg-icons";

export const Homepage: React.FC = () => {
    const [file, setFile] = useState<any>();

    function handleChange(event: any) {
        console.log(event.target.files[0]);
        setFile(event.target.files[0]);
    }
    async function handleSubmit(event: any) {
        event.preventDefault();
        const data = await formatWordDocument(file);
        const outputFileName = `RESULT.docx`;
        const url = URL.createObjectURL(new Blob([data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", outputFileName);
        document.body.appendChild(link);
        link.click();
    }

    return (
        // <div className="App">
        //     <form onSubmit={handleSubmit}>
        //         <h1>React File Upload</h1>
        //         <input type="file" onChange={handleChange} />
        //         <button type="submit">Upload</button>
        //     </form>
        // </div>
        <div className="main_container">
            <div style={{paddingTop: "300px"}} className="jiggle">Оформление по ГОСТу</div>
            <div style={{paddingTop: "12px", fontSize: "20px"}} className="gost_req">
                Оформите свою работу согласно требованиям ГОСТ-2022.3e.15
            </div>

            <form onSubmit={handleSubmit}>
                <div style={{ textAlign: "center", paddingTop: "128px" }}>
                    <label className="input-file">
                        <input type="file" name="file" onChange={handleChange} />
                        <span className="input-file-btn">
                            ВЫБРАТЬ .DOCX ФАЙЛ
                        </span>
                    </label> {" "} 
                    <Button style={{width: "80px", height: "80px", transform: "translateY(-2px)"}} htmlType="submit" size="large" type="primary">
                        <FontAwesomeIcon size="2x" icon={faPaperPlane} />
                    </Button>
                </div>
            </form>
        </div>
    );
};
