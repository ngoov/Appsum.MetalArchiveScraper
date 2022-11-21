import { format } from "date-fns";
import { NextPage } from "next";
import Link from "next/link";
import { useRouter } from "next/router";
import { BandDto } from "../../dtos";
import { useGetBandsByGenreId } from "../../hooks/bands/useBands";

const Index: NextPage = () => {
    const router = useRouter();
    const { genre } = router.query;
    const genreId = genre as string;
    const { data: bands, isLoading } = useGetBandsByGenreId(genreId);
    if (isLoading) {
        return <div>Loading...</div>;
    }
    return (
        <div>
            <h1>
                Bands (<Link href={"/"}>Back</Link>)
            </h1>
            <div
                style={{
                    display: "grid",
                    gridAutoFlow: "column",
                    gridTemplateRows: "repeat(50, 1fr)",
                }}
            >
                {bands.map((band: BandDto) => (
                    <span key={band.id}>
                        {band.name} (
                        {format(new Date(band.newestAlbumDate), "yyyy-MM-dd")})
                    </span>
                ))}
            </div>
        </div>
    );
};
export default Index;
