import React, { useEffect, useState,useRef  } from 'react';
import { StyleSheet, ImageBackground, Text, View, FlatList,TouchableOpacity } from 'react-native'; 
import axios from 'axios'; 
import SelectDropdown from 'react-native-select-dropdown'

export default function Visita({ route, navigation }) {
  const [visitaData, setVisitaData] = useState([]);
  const { utenteData } = route.params;
  const [selectedMonth, setSelectedMonth] = useState(null);
  const selectRef = useRef();

  const months = [
    'Janeiro', 'Fevereiro', 'Março', 'Abril',
    'Maio', 'Junho', 'Julho', 'Agosto',
    'Setembro', 'Outubro', 'Novembro', 'Dezembro'
  ];

  useEffect(() => {
    axios.get(`http://192.168.1.42:8800/visita?Utente_idUtente=${utenteData.idUtente}`)
      .then(visitaResponse => {
        const filteredVisitas = selectedMonth
          ? visitaResponse.data.filter(item => new Date(item.Data_HoraVisita).getMonth() === months.indexOf(selectedMonth))
          : visitaResponse.data;

        setVisitaData(filteredVisitas);
      })
      .catch(error => {
        console.error('Erro ao buscar visitas do utente:', error);
      });
  }, [selectedMonth]);

  const clearSelection = () => {
    setSelectedMonth(null);
    selectRef.current.reset();
  };

  return (
    <View style={styles.container}>
      <ImageBackground source={require('../Image/Image2.png')} style={[styles.Image2, styles.bottomImage]} />
 
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
        data={visitaData}
        keyExtractor={(item, index) => (item.id ? item.id.toString() : index.toString())}
        renderItem={({ item }) => (
          <View style={styles.View1} key={item.id ? item.id.toString() : null}>
            <Text style={styles.texto}>Nome do familiar: {item.Nome_Familiar}</Text>
            <Text style={styles.texto}>Data e Hora: {new Date(item.Data_HoraVisita).toLocaleString()}</Text>
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
  View1:{
    backgroundColor:'rgba(113, 161, 255, 0.5)',
    padding: 10,
    borderWidth: 5,
    borderColor:'white',
    borderRadius: 30,
    justifyContent: 'center',
    alignItems: 'center',
  },
  texto:{
    marginBottom:10,
    fontSize:15
    
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
