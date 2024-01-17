import React, { useEffect, useState, useRef } from 'react';
import { StyleSheet, ImageBackground, Text, View, FlatList, TouchableOpacity } from 'react-native';
import axios from 'axios';
import SelectDropdown from 'react-native-select-dropdown';

export default function PlanoPagamento({ route, navigation }) {
  const [pagamentoData, setPagamentoData] = useState([]);
  const { utenteData } = route.params;
  const [selectedYear, setSelectedYear] = useState(null);
  const selectRef = useRef();

  const years = [
    2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009,
    2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019,
    2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029,
    2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037, 2038, 2039,
    2040, 2041, 2042, 2043, 2044, 2045, 2046, 2047, 2048, 2049,
    2050
  ];

  useEffect(() => {
    axios.get(`http://192.168.1.42:8800/pagamento?Utente_idUtente=${utenteData.idUtente}`)
      .then(pagamentoResponse => {
        const filteredPagamentos = selectedYear
          ? pagamentoResponse.data.filter(item => new Date(item.data_limitel).getFullYear() === selectedYear)
          : pagamentoResponse.data;

        setPagamentoData(filteredPagamentos);
      })
      .catch(error => {
        console.error('Erro ao buscar pagamentos do utente:', error);
      });
  }, [selectedYear]);

  const clearSelection = () => {
    setSelectedYear(null);
    selectRef.current.reset();
  };

  return (
    <View style={styles.container}>
      <ImageBackground source={require('../Image/Image2.png')} style={[styles.Image2, styles.bottomImage]} />

      <SelectDropdown
        ref={selectRef}
        data={years}
        onSelect={(year) => setSelectedYear(year)}
        buttonTextAfterSelection={(year) => year.toString()}
        rowTextForSelection={(year) => year.toString()}
        defaultButtonText="Selecione um ano"
      />

      <View style={styles.clearButtonContainer}>
        <TouchableOpacity onPress={clearSelection}>
          <Text style={styles.clearButtonText}>Limpar Seleção</Text>
        </TouchableOpacity>
      </View>

      <FlatList
        data={pagamentoData}
        keyExtractor={(item, index) => (item.idPagamento ? item.idPagamento.toString() : index.toString())}
        renderItem={({ item }) => (
          <View style={styles.View1} key={item.idPagamento ? item.idPagamento.toString() : null}>
            <Text style={styles.texto}>Valor: {item.valor}</Text>
            <Text style={styles.texto}>Data Limite: {new Date(item.data_limitel).toLocaleString()}</Text>
            <Text style={styles.texto}>Estado: {item.estado}</Text>
          </View>
        )}
      />

    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
  },
  TEXTO: {
    marginBottom: 7,
    borderWidth: 1,
    borderColor: 'black',
    padding: 6,
    width: 'auto',
    borderRadius: 8,
    textAlign: 'center',
  },
  PerfilUtente1: {
    flex: 1,
    padding: 10,
    marginBottom: 50,
    marginTop: 10,
  },
  Image2: {
    height: 205,
    width: '110%',
    padding: 10,
    marginTop: 20,
  },
  bottomImage: {
    position: 'absolute',
    bottom: -125,
  },
  View1: {
    backgroundColor:'rgba(113, 161, 255, 0.5)',
    padding: 10,
    borderWidth: 5,
    borderColor:'white',
    borderRadius: 30,
    justifyContent: 'center',
    alignItems: 'center',
  },
  texto: {
    marginBottom: 10,
    fontSize: 15,
  },
  clearButtonContainer: {
    marginTop: 10,
    alignItems: 'center',
  },
  clearButtonText: {
    color: 'blue',
    fontSize: 16,
    textDecorationLine: 'underline',
  },
});
