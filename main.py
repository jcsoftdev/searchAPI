import sys
import requests
def search(busqueda, KEY, EngineID):
    url = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx="+EngineID+"&q="+busqueda
    response = requests.get(url)
    resJson = response.json()
    return (resJson['queries']['request'][0]['totalResults'])

def getResults(busqueda):
    EngineIdG = "010556043604774410724:xvljiggke1i"
    EngineIdB = "010556043604774410724:fzoekgp3pmq"
    KEY = "AIzaSyAJuxgY1dQGQV45L1nIOoxLjJd7Cy-xA1M"
    resBing = search(busqueda, KEY, EngineIdB)
    resGoogle = search(busqueda, KEY, EngineIdG)
    return [resBing, resGoogle]
   
if __name__ == "__main__":
    # obteniendo todos los parametros por consola
    busquedas = sys.argv[1:]
    if len(busquedas) == 0:
        print('nada que buscar')
    else:
        contGoogle = 0
        contBing = 0
        for busqueda in busquedas:
            result = getResults(busqueda)
            print("______________________________________________")
            print('{}: Bing   => {}'.format(busqueda,result[0]))
            print('{}: Google => {}'.format(busqueda,result[1]))
            if result[0]>result[1]:
                contBing += 1
                print('{} Bing Winner'.format(busqueda))
            elif result[0]<result[1]:
                contGoogle += 1
                print('{} Google Winner'.format(busqueda))
        print("______________________________________________")
        print('Total results: Google {} vs Bing {}'.format(contGoogle,contBing))
        if contGoogle>contBing:
            print('Total results: Google winner')
        elif contGoogle<contBing:
            print('Total results: Bing winner')