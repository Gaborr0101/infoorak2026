let filenev = "vonat.txt";
betolt();
const adatok = [];
function betolt()
{
    fetch(filenev)
    .then(x => x.text())
    .then(x => {
        console.log(x);
        const regex = /^(?<vonatId>\d+)\s(?<allomasId>\d+)\s(?<ora>\d+)\s(?<perc>\d+)\s(?<tipus>[IE])$/gm;

        let match;
        while ((match = regex.exec(x)) !== null){
            adatok.push(match.groups);
        }
        console.log(adatok);
    });
}

function feladat2()
{
    console.log("adasd");
    
}