import axios, { AxiosRequestConfig } from 'axios';
import { WORD_SERVICE } from './configuration';


export async function formatWordDocument(file: any): Promise<any> {
    const url = `${WORD_SERVICE}/format-document`;
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