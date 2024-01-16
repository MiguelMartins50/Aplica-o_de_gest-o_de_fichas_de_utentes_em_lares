import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground ,FlatList, Alert, } from 'react-native';
import axios from 'axios'; 
import SelectDropdown from 'react-native-select-dropdown'
export default function VisitasFamiliar({navigation, route}) {
  const FamiliarData = route.params && route.params.FamiliarData;
  const familiarID = FamiliarData.idFamiliar;
  const [VisitaData, setVisitaData] = useState([]);
  const [selectedYear, setSelectedYear] = useState(null);

  const years = [
    2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009,
    2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019,
    2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029,
    2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037, 2038, 2039,
    2040, 2041, 2042, 2043, 2044, 2045, 2046, 2047, 2048, 2049,
    2050
  ];
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
  const [selectedDeleteId, setSelectedDeleteId] = useState(null);
  const handleAdd = () => {
    console.log('aqui');
    navigation.navigate('AddVisitas',{FamiliarData});
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
          onPress: () => confirmDelete(idVisita),
          style: 'destructive',
        },
      ],
      { cancelable: true }
    );
  };
  const confirmDelete = (idVisita) => {
    axios
      .delete(`http://192.168.1.80:8800/visita/${idVisita}`)
      .then(() => {
        // Refresh the data after deletion
        axios
          .get(`http://192.168.1.80:8800/Visita?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
          .then((VisitaResponse) => {
            const filteredVisita = selectedYear
              ? VisitaResponse.data.filter((item) => new Date(item.data).getFullYear() === selectedYear)
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
    axios.get(`http://192.168.1.80:8800/Visita?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
      .then(VisitaResponse => {
        if (VisitaResponse.data && Array.isArray(VisitaResponse.data)) {
          const filteredVisita = selectedYear
          ? VisitaResponse.data.filter(item => new Date(item.data).getFullYear() === selectedYear)
          : VisitaResponse.data; 
          console.log(VisitaResponse.data);
          console.log('Filtered Payments:', filteredVisita);
          console.log('Selected Month Index:', selectedYear);
          setVisitaData(filteredVisita);
          console.log('PagamentoData:', VisitaData);

        } else {
          console.log('Consulta do utente não retornou dados válidos:', VisitaResponse.data);
        }
      })
      .catch((error) => {
        console.error('Erro ao buscar Consulta do utente:', error);
      });
  }, [selectedYear]);
  

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
          <Image source={require('../Image/add.png')} resizeMode="cover" style={styles.image3}/>
        </TouchableOpacity>
        <View style={styles.View1}>
          <View style={styles.Text2}>
            <Text style={styles.ButtonText}>Visitas</Text>
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
      </View>
    }
    data={VisitaData}
    keyExtractor={(item) => (item.id ? item.id.toString() : String(Math.random()))}
    renderItem={({ item }) => (
      <View style={styles.View3}>
        <Text style={styles.texto}>Utente: {item.nomeutente}</Text>
        <Text style={styles.texto}>
          Dia: {('0' + (new Date(item.data).getDate())).slice(-2)}/
          {('0' + (new Date(item.data).getMonth() + 1)).slice(-2)}
        </Text>
        <Text style={styles.texto}>
          Hora: {('0' + new Date(item.data).getHours()).slice(-2)}/
          {('0' + new Date(item.data).getMinutes()).slice(-2)}
        </Text>
          <TouchableOpacity style={styles.deleteButton} onPress={() => handleDeleteVisita(item.idVisita)}>
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