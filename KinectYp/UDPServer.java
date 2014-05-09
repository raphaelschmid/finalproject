import java.awt.*;
import java.awt.event.KeyEvent;
import java.lang.reflect.Field;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

class UDPServer
{
    static Robot robot;

    public static void main(String args[]) throws Exception {
        robot = new Robot();
        DatagramSocket serverSocket = new DatagramSocket(9876);


        while (true) {
            byte[] receiveData = new byte[1024];
            byte[] sendData;
            DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
            serverSocket.receive(receivePacket);
            String sentence = new String(receivePacket.getData());
            System.out.println(">>>RECEIVED: " + sentence);
            parseMessage(sentence);
            InetAddress IPAddress = receivePacket.getAddress();
            int port = receivePacket.getPort();
            String capitalizedSentence = sentence.toUpperCase();
            sendData = capitalizedSentence.getBytes();
            DatagramPacket sendPacket =
                    new DatagramPacket(sendData, sendData.length, IPAddress, port);
            serverSocket.send(sendPacket);
        }
    }

    public static void parseMessage(String message) throws InterruptedException, NoSuchFieldException, IllegalAccessException {
        for (String messagePart : message.split(";")) {
            messagePart = messagePart.trim();
            if (messagePart.length() >= 2) {
                if (messagePart.matches("^\\d+$")) {
                    System.out.format("sleeping: %s ms.\n", messagePart);
                    Thread.sleep(Integer.valueOf(messagePart));
                } else {
                    if (messagePart.substring(0, 1).equals("d")) {
                        System.out.format("pressing:  %s ms.\n", messagePart.charAt(1));
                        robot.keyPress(getKeyEvent(messagePart.charAt(1)));
                    } else if (messagePart.substring(0, 1).equals("u")) {
                        System.out.format("releasing: %s ms.\n", messagePart.charAt(1));
                        robot.keyRelease(getKeyEvent(messagePart.charAt(1)));
                    }
                }
            }
        }
        System.out.println();
    }

    public static int getKeyEvent(Character c) throws NoSuchFieldException, IllegalAccessException {
        String prefix;
        if (Character.isDigit(c)) {
            prefix = "VK_NUMPAD" + c;
        } else {
            prefix = "VK_" + Character.toUpperCase(c);
        }
        Field f = KeyEvent.class.getField(prefix);
        f.setAccessible(true);
        return (Integer) f.get(null);
    }
}