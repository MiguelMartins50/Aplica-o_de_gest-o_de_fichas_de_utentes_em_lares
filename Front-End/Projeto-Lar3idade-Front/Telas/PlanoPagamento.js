import React, { useEffect, useState, useRef } from 'react';
import { StyleSheet, Text, View, FlatList, TouchableOpacity } from 'react-native';
import axios from 'axios';
import SelectDropdown from 'react-native-select-dropdown';

export default function PlanoPagamento({ route, navigation }) {
  const [pagamentoData, setPagamentoData] = useState([]);
  const { utenteData } = route.params;
  const [selectedMonth, setSelectedMonth] = useState(null);
  const selectRef = useRef();

  const months = [
    'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
  ];

  useEffect(() => {
    axios.get(`http://192.168.1.15:8800/pagamento?Utente_idUtente=${utenteData.idUtente}`)
      .then(pagamentoResponse => {
        const filteredPagamentos = selectedMonth
          ? pagamentoResponse.data.filter(item => new Date(item.data_limitel).getMonth() === months.indexOf(selectedMonth))
          : pagamentoResponse.data;

        setPagamentoData(filteredPagamentos);
      })
      .catch(error => {
        console.error('Erro ao buscar pagamentos do utente:', error);
      });
  }, [selectedMonth]);

  const clearSelection = () => {
    setSelectedMonth(null);
    selectRef.current.reset();
  };

  return (
    <View style={styles.container}>
      <SelectDropdown
        ref={selectRef}
        data={months}
        onSelect={(month) => setSelectedMonth(month)}
        buttonTextAfterSelection={(month) => month}
        rowTextForSelection={(month) => month}
        defaultButtonText="Selecione um mês"
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
