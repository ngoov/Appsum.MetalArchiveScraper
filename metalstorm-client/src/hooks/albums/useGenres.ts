import axios from "axios";
import { useQuery } from "react-query";

const fetchGenres = async (_limit = 10) => {
    const result = await axios.get("https://localhost:7272/api/genres");
    return result.data;
};

const useGenres = (limit = 10) => {
    return useQuery(["genres", limit], () => fetchGenres(limit));
};

export { useGenres };
