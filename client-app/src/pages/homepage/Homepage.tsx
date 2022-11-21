import { message, Upload, Spin } from "antd";
import React, { useEffect } from "react";
import {
    formatWordDocument,
    testNet,
} from "../../data-contexts/wordDataContext";
import { useAsyncData } from "../../hooks/useAsyncData";
import "./Homepage.scss";

export const Homepage: React.FC = () => {
    const { data, loading, error } = useAsyncData(
        async () => {
            const result = await testNet(7);
            return result;
        },
        5,
        []
    );
    useEffect(() => {
        if (error) {
            message.error("TI HUYLO!!!");
        }
    }, [error])
    console.log(data);

    const handleChange = async (info: any) => {
        await formatWordDocument(info.file);
    }

    return (
        <>
            <div className="title">TITLE</div>
            <Spin spinning={loading}><div className="text">{data}</div></Spin>
            
            <Upload onChange={handleChange}>Upload your file</Upload>
        </>
    );
};
