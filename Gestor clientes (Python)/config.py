#Contendra constantes de configuracion

import sys


DATABASE_PATH = "clientes.csv" 

if "pytest" in sys.argv[0]:  #Si ejecumtanos pytest, se encontrara aqui
    DATABASE_PATH = "tests/clientes_test.csv"





