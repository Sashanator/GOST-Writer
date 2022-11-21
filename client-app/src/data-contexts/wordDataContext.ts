import axios, { AxiosRequestConfig } from 'axios';
import { WORD_SERVICE } from './configuration';


export async function formatWordDocument(file: any): Promise<any> {
    const url = `${WORD_SERVICE}word/format-document`;
    const formData = new FormData();
    formData.append('doc', file);
    const config: AxiosRequestConfig = {
        headers: {
          'content-type': 'multipart/form-data',
        },
        responseType: 'arraybuffer'
      };
    const { data } = await axios.post(url, formData, config);
    return data;
}

export async function testNet(a: any): Promise<number> {
    const { data } = await axios.post<number>(`${WORD_SERVICE}word/test/${a}`)
    return data;
}

export async function exportCalculationTaskResultToExcel() {
    const headers = {'Content-Type': 'blob'};
    const config: AxiosRequestConfig = {
        method: "POST",
        url: `${WORD_SERVICE}word/format-document`,
        responseType: "arraybuffer",
        headers,
    };
    const { data } = await axios(config);
    return data;
}