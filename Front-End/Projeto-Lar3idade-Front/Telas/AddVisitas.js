import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground,  } from 'react-native';
import axios from 'axios'; 
import SelectDropdown from 'react-native-select-dropdown'
import DateTimePicker from '@react-native-community/datetimepicker';
import moment from 'moment';
import { Alert } from 'react-native';

export default function AddVistas({navigation, route}) {
  const FamiliarData = route.params && route.params.FamiliarData;
  const familiarID = FamiliarData.idFamiliar;
  const [VisitaData, setVisitaData] = useState([]);
  const [UFData, setUFData] = useState([]);
  const [UData, setUData] = useState([]);
  const [NomeData, setNomeData] = useState([]);
  const [combinedDateTime, setCombinedDateTime] = useState(null);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [selectedTime, setSelectedTime] = useState(new Date());
  const [showDatePicker, setShowDatePicker] = useState(false);
  const [showTimePicker, setShowTimePicker] = useState(false);  
  const [SelectedUtente, setSelectedUtente] = useState('');

  const [VData, setVDATA] = useState([]);

  useEffect(() => {
    console.log('Selected Utente :', SelectedUtente);
    // Fetch visits associated with the selected Utente
    axios.get(`http:/192.168.1.80:8800/visita?Utente_idUtente=${SelectedUtente}`)
      .then(response => {
        setVDATA(response.data);
      })
      .catch(error => {
        console.error('Error fetching visits for Utente:', error);
      });
  }, [SelectedUtente]);

  useEffect(() => {
    console.log('Updated VData:', VData);
    
  }, [VData]);
  
  useEffect(() => {
    axios.get(`http://192.168.1.80:8800/utente_familiar?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
      .then(consultaResponse => {
        setUFData(consultaResponse.data);
        if (consultaResponse.data && consultaResponse.data[0] && consultaResponse.data[0].Utente_idUtente) {
          const utenteId = consultaResponse.data[0].Utente_idUtente;
  
          axios.get(`http://192.168.1.80:8800/utente?idUtente=${utenteId}`)
            .then(utenteResponse => {
              setUData(utenteResponse.data);
              
            })
            .catch(error => {
              console.error('Erro ao buscar Consulta do utente:', error);
            });
        } else {
          console.error('Utente_idUtente not found in the response:', consultaResponse.data);
        }
      })
      .catch(error => {
        console.error('Erro ao buscar Consulta do utente familiar:', error);
      });
      console.log('UFData:');

      console.log(UFData);
      console.log('UData:');

      console.log(UData);
    }, [FamiliarData]);
    useEffect(() => {
        console.log('Updated UFData:', UFData);
      }, [UFData]);
      useEffect(() => {
        console.log('Updated UData:', UData);
        
      }, [UData]);
      useEffect(() => {
        console.log('Selected Utente :', SelectedUtente);
        
      }, [SelectedUtente]);
      const handleDateChange = (event, selected) => {
        const currentDate = selected || selectedDate;
        setShowDatePicker(false);
        setSelectedDate(currentDate);
        updateCombinedDateTime(currentDate ,currentTime);
      };
      const handleTimeChange = (event, selected) => {
        const currentTime = selected || selectedTime;
        setShowTimePicker(false);
        setSelectedTime(currentTime);
        updateCombinedDateTime(currentDate ,currentTime);
      };
      useEffect(() => {
        setCombinedDateTime(`${selectedDate.toLocaleDateString()} ${selectedTime.toLocaleTimeString()}`);
      }, [selectedDate, selectedTime]);

      const handleAddVisita = () => {
        const [datePart, timePart] = combinedDateTime.split(' ');
    
        const [day, month, year] = datePart.split('/');
        const [hour, minute, second] = timePart.split(':');
    
        // Create a new Date object using the extracted components
        const newDate = new Date(`${year}-${month}-${day}T${hour}:${minute}:0`);
    
        // Adjust the date by adding 30 minutes
        const halfHourLater = new Date(newDate.getTime() + 30 * 60000); // Adding 30 minutes (30 * 60 * 1000 milliseconds)
    
        // Format the dates in the desired format (ISO 8601)
        const formattedDate = newDate.toISOString();
        const formattedHalfHourLater = halfHourLater.toISOString();
        console.log(formattedDate);
        console.log(formattedHalfHourLater);
        const visitaData = {
            Utente_idUtente: SelectedUtente,
            data: formattedDate,
            Familiar_idFamiliar: FamiliarData.idFamiliar,
        };
        axios.get(`http://192.168.1.80:8800/Visita?start=${formattedDate}&end=${formattedHalfHourLater}`)
            .then(consultaResponse => {
                if (consultaResponse.data.length === 0) {
                    axios.post('http://192.168.1.80:8800/visita', visitaData)
                        .then(response => {
                            console.log('Visita added successfully:', response.data);
                            Alert.alert(
                                'Sucesso',
                                'Visita adicionada com sucesso.',
                                [
                                    { text: 'OK', onPress: () => navigation.navigate('VisitasFamiliar', FamiliarData, familiarID) }
                                ],
                                { cancelable: false }
                            );
                        })
                        .catch(error => {
                            console.error('Error adding visita:', error);
                        });
                } else {
                    console.log('Visitas nao podem ser subrepostas');
                    Alert.alert(
                        'Horário Ocupado',
                        'Escolha outra hora para marcar a visita porque o horario que escolheu já está ocupado.',
                        [
                            { text: 'OK' }
                        ],
                        { cancelable: false }
                    );
                }
            })
            .catch(error => {
                console.error('Error checking visita data:', error);
            });
    };
    
    
    
    
    
    
return (<View style={styles.container}>
    <View style={styles.Text2}>
            <Text style={styles.ButtonText}>Visitas</Text>
          </View>
          <SelectDropdown
        data={UData.map(utente => ({ id: utente.idUtente, name: utente.nome }))}
        onSelect={(selectedItem, index) => {
        
          const selectedId = selectedItem.id;
          console.log('Selected Utente ID:', selectedId);
          setSelectedUtente(selectedId);
          
          
        }}
        buttonTextAfterSelection={(selectedItem, index) => selectedItem.name}
        rowTextForSelection={(selectedItem, index) => selectedItem.name}
        defaultButtonText="Selecione um nome"
      />
    <TouchableOpacity
      style={styles.Button}
      onPress={() => setShowDatePicker(true)}
    >
      <Text style={styles.ButtonText}>Selecionar Dia</Text>
    </TouchableOpacity>

 
    <Text>Dia Selecionado: {selectedDate.toLocaleDateString()}</Text>
    <TouchableOpacity style={styles.Button} onPress={() => setShowTimePicker(true)}>
        <Text style={styles.ButtonText}>Selecionar Hora</Text>
    </TouchableOpacity>

     
      <Text>Hora Selecionada: {selectedTime.toLocaleTimeString()}</Text>
    <View style={styles.View3}>
        <Text>Horario Selecionado: {combinedDateTime}</Text>
    </View>    
      
    <TouchableOpacity style={styles.Button} onPress={handleAddVisita}>
        <Text style={styles.ButtonText}>Adicionar</Text>
    </TouchableOpacity>
    
    {showDatePicker && (
      <DateTimePicker
        value={selectedDate}
        mode="date"
        display="default"
        onChange={handleDateChange}
      />
    )}

    {showTimePicker && (
        <DateTimePicker
          value={selectedTime}
          mode="time"
          display="default"
          onChange={handleTimeChange}
        />
      )}
    <View style={styles.Imag}>
      <Image source={require('../Image/Image2.png')} style={styles.Image2} />
    </View>
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
  Button: {
    backgroundColor: '#3498db',
    paddingVertical: 10,
    paddingHorizontal: 20, 
    borderRadius: 8,
    height: 40,
    width: 300,  
    marginBottom: 20,
    marginTop: 20,
    justifyContent: 'center',  
    alignItems: 'center',    
  },
  ButtonText: {
    color: '#fff',
    fontSize: 17,

  },
  Image2: {
    height:205,
    width:410,
    padding:20,
    marginBottom:-200
  },
  Imag:{
    padding:50
  },
  Imag1:{
    padding:350,
    
  },
  sairButton: {
    backgroundColor: '#3498db',
    padding: 2,
    width:60,
    borderRadius: 5,
    marginLeft:310,
    top:-50,
    margin: 25,
  },
  sairButtonText: {
    color: 'black',
    fontWeight: 'bold',
    textAlign:'center'
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
});