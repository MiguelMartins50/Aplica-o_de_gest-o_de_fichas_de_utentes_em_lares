import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground, FlatList } from 'react-native';
import axios from 'axios'; 
import SelectDropdown from 'react-native-select-dropdown'

export default function PagamentosFamiliar({ navigation, route }) {
  const FamiliarData = route.params && route.params.FamiliarData;
  const [PagamentoData, setPagamentoData] = useState([]);
  const [selectedYear, setSelectedYear] = useState(null);

  const years = [
    2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009,
    2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019,
    2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029,
    2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037, 2038, 2039,
    2040, 2041, 2042, 2043, 2044, 2045, 2046, 2047, 2048, 2049,
    2050
  ];
  useEffect(() => {
    axios.get(`http://192.168.1.42:8800/pagamento?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
      .then(PagamentoResponse => {
        if (PagamentoResponse.data && Array.isArray(PagamentoResponse.data)) {
          const filteredPagamento = selectedYear
          ? PagamentoResponse.data.filter(item => new Date(item.data_limitel).getFullYear() === selectedYear)
          : PagamentoResponse.data; 
          console.log(PagamentoResponse.data);
          console.log('Filtered Payments:', filteredPagamento);
          console.log('Selected Month Index:', selectedYear);
          setPagamentoData(filteredPagamento);
          console.log('PagamentoData:', PagamentoData);

        } else {
          console.log('Consulta do utente não retornou dados válidos:', PagamentoResponse.data);
        }
      })
      .catch((error) => {
        console.error('Erro ao buscar Consulta do utente:', error);
      });
  }, [selectedYear]);
  

  useEffect(() => {
    console.log('PagamentoData:');
    console.log(PagamentoData);

  }, [PagamentoData]);

return (
 
      <FlatList
        style={styles.container}
        ListHeaderComponent={
          <View style={styles.View1}>
            <View style={styles.Text2}>
              <Text style={styles.ButtonText}>Planos de Pagamento</Text>
            </View>
            <SelectDropdown
            data={years}
            onSelect={(year, index) => setSelectedYear(year)}
            buttonTextAfterSelection={(year, index) => year}
            rowTextForSelection={(year, index) => year}
            defaultButtonText="Selecione um ano"
            />

            <View style={styles.clearButtonContainer}>
              <TouchableOpacity onPress={() => setSelectedYear(null)}>
                <Text style={styles.clearButtonText}>Limpar Seleção</Text>
              </TouchableOpacity>
            </View>
            
          </View>
        }
        data={PagamentoData}
        keyExtractor={(item) => (item.id ? item.id.toString() : String(Math.random()))}
        renderItem={({ item }) => (
          <View style={styles.View3}>
            <Text style={styles.texto}>Valor: {item.valor}</Text>
            <Text style={styles.texto}>Data e Hora: {new Date(item.data_limitel).toLocaleString()}</Text>
            <Text style={styles.texto}>Estado: {item.estado}</Text>
          </View>
        )}
      />
    
);

}

const styles = StyleSheet.create({
  container: {  
    flex: 1,
    backgroundColor: '#fff',
    padding: 15,   
  },
  image: {
    width: 100,
    height: 100,
    resizeMode: 'cover',
    borderRadius: 50,
  },
  View1: {
    marginTop: 1,
    padding: 10,
    justifyContent: 'center',
    alignItems: 'center',
  },
  View3: {
    backgroundColor:'rgba(113, 161, 255, 0.5)',
    padding: 10,
    borderWidth: 5,
    borderColor:'white',
    borderRadius: 30,
    justifyContent: 'center',
    alignItems: 'center',
  },
  NomeGrauContainer: {
    marginBottom: 20,
    alignItems: 'center',
  },
  NomeText: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  GrauText: {
    fontSize: 16,
  },
  blackbar: {
    width: 500,
    height: 10,
    backgroundColor: 'black',
    marginTop: 20,
  },
  View2: {
    backgroundColor:'rgba(113, 161, 255, 0.5)',
    padding: 10,
    marginTop: 20,
    width: 300,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: 'black',
    borderRadius: 5,
  },
  texto: {
    fontSize: 16,
    marginBottom: 10,
  },
  Imag: {
    padding: 20,
    marginTop: 20,
  },
  Image2: {
    height: 205,
    width: 410,
    padding: 20,
    marginTop:200,
    marginBottom: -300,
  },
});