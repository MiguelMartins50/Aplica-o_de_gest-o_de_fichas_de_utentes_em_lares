import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground, FlatList, Alert } from 'react-native';
import axios from 'axios';
import SelectDropdown from 'react-native-select-dropdown';

export default function VisitasFamiliar({ navigation, route }) {
  const FamiliarData = route.params && route.params.FamiliarData;
  const [VisitaData, setVisitaData] = useState([]);
  const [selectedMonth, setSelectedMonth] = useState(null);

  const months = [
    'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
  ];
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
  const [selectedDeleteId, setSelectedDeleteId] = useState(null);

  const handleAdd = () => {
    console.log('aqui');
    navigation.navigate('AddVisitas', { FamiliarData });
  };

  const handleDeleteVisita = (idVisita) => {
    Alert.alert(
      'Confirm Delete',
      'Tem a certeza que quere Cancelar esta visita?',
      [
        {
          text: 'Não',
          style: 'cancel',
        },
        {
          text: 'Sim',
          onPress: () => { console.log(idVisita); confirmDelete(idVisita); },
          style: 'destructive',
        },
      ],
      { cancelable: true }
    );
  };

  const confirmDelete = (idVisita) => {
    console.log(idVisita);
    axios
      .delete(`http://192.168.1.42:8800/visita/${idVisita}`)
      .then(() => {

        axios
          .get(`http://192.168.1.42:8800/Visita?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
          .then((VisitaResponse) => {
            const filteredVisita = selectedMonth
              ? VisitaResponse.data.filter((item) => new Date(item.Data_HoraVisita).getMonth() === selectedMonth)
              : VisitaResponse.data;
            setVisitaData(filteredVisita);
          })
          .catch((error) => {
            console.error('Error fetching Visita data:', error);
          });
      })
      .catch((error) => {
        console.error('Error deleting Visita:', error);
      })
      .finally(() => {
        setShowDeleteConfirmation(false);
        setSelectedDeleteId(null);
      });
  };

  useEffect(() => {
    axios.get(`http://192.168.1.42:8800/Visita?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
      .then(VisitaResponse => {
        if (VisitaResponse.data && Array.isArray(VisitaResponse.data)) {
          const filteredVisita = selectedMonth
            ? VisitaResponse.data.filter(item => new Date(item.Data_HoraVisita).getMonth() === selectedMonth)
            : VisitaResponse.data;
          console.log(VisitaResponse.data);
          console.log('Filtered Payments:', filteredVisita);
          console.log('Selected Month Index:', selectedMonth);
          setVisitaData(filteredVisita);
          console.log('PagamentoData:', VisitaData);

        } else {
          console.log('Consulta do utente não retornou dados válidos:', VisitaResponse.data);
        }
      })
      .catch((error) => {
        console.error('Erro ao buscar Consulta do utente:', error);
      });
  }, [selectedMonth]);

  useEffect(() => {
    console.log('PagamentoData:');
    console.log(VisitaData);

  }, [VisitaData]);

  return (
    <FlatList
      style={styles.container}
      ListHeaderComponent={
        <View style={styles.View4}>
          <TouchableOpacity onPress={handleAdd}>
            <Image source={require('../Image/add.png')} resizeMode="cover" style={styles.image3} />
          </TouchableOpacity>
          <View style={styles.View1}>
            <View style={styles.Text2}>
              <Text style={styles.ButtonText}>Visitas</Text>
            </View>
            <SelectDropdown
              data={months}
              onSelect={(month, index) => setSelectedMonth(index)}
              buttonTextAfterSelection={(month, index) => month}
              rowTextForSelection={(month, index) => month}
              defaultButtonText="Selecione um mês"
            />

            <View style={styles.clearButtonContainer}>
              <TouchableOpacity onPress={() => setSelectedMonth(null)}>
                <Text style={styles.clearButtonText}>Limpar Seleção</Text>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      }
      data={VisitaData}
      keyExtractor={(item) => (item.id ? item.id.toString() : String(Math.random()))}
      renderItem={({ item }) => (
        <View style={styles.View3}>
          <Text style={styles.texto}>Utente: {item.nomeutente}</Text>
          <Text style={styles.texto}>
            Dia: {('0' + (new Date(item.Data_HoraVisita).getDate())).slice(-2)}/
            {('0' + (new Date(item.Data_HoraVisita).getMonth() + 1)).slice(-2)}
          </Text>
          <Text style={styles.texto}>
            Hora: {('0' + new Date(item.Data_HoraVisita).getHours()).slice(-2)}/
            {('0' + new Date(item.Data_HoraVisita).getMinutes()).slice(-2)}
          </Text>
          <TouchableOpacity style={styles.deleteButton} onPress={() => { console.log(item.idVisita); handleDeleteVisita(item.idVisita); }} >
            <Text style={styles.deleteButtonText}>Cancelar</Text>
          </TouchableOpacity>
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
    marginLeft:-50
  },
  View4: {
    flexDirection:'row',
    padding: 5,
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
    alignItems: 'center',position: 'relative',
    marginTop: 10,
  },
  deleteButton: {
    backgroundColor: 'white',
    padding: 10,
    borderRadius: 5,
    marginTop: 10,
    alignItems: 'center',
  },
  deleteButtonText: {
    color: 'black',
    fontWeight: 'bold',
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
  image3: {
    height: 30,
    width: 30,
    marginRight: 50
    
  },
  ButtonText:{
    fontSize: 25,

  }
});